using Dapper;
using Hinox.Data.Mssql;
using Hinox.Data.Mssql.Dal.Dto;
using Hinox.Data.Mssql.Statement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Data.Mssql
{
    public abstract class BaseMssqlDal<T> : IMssqlDao<T> where T : BaseMssqlDto
    {
        //private IDbConnection connection;
        private MssqlDtoSchema dtoSchema;
        private string connectionString;
        public BaseMssqlDal(IDatabaseFactory dbFactory)
        {
            var type = typeof(T);

            dtoSchema = MssqlDtoSchema.GetSchema(type);

            connectionString = dbFactory.GetConnectionString<T>();
        }
        #region async region
        public async Task<IEnumerable<T>> FindAsync(object condition)
        {
            string statement = MssqlQueryBuilder.BuildSelectStatement<T>(condition);
            var result = await QueryAsync<T>(statement, condition);
            return result;
        }
        public async Task<T> GetFirstAsync(object condition)
        {
            string statement = MssqlQueryBuilder.BuildSelectStatement<T>(condition);
            var result = await QueryAsync<T>(statement, condition);
            if (result.Any())
                return result.ElementAt(0);
            return null;
        }
        public async Task<int> InsertAsync(T entity)
        {
            string statement = MssqlQueryBuilder.BuildInsertStatement<T>();
            var result = await ExecuteAsync(statement, entity);
            return result;
        }
        public async Task<int> UpdateAsync(T entity)
        {
            string statement = MssqlQueryBuilder.BuildSingleUpdateStatement<T>();
            var result = await ExecuteAsync(statement, entity);
            return result;
        }
        public async Task<int> DeleteAsync(T entity)
        {
            string statement = MssqlQueryBuilder.BuildSingleUpdateStatement<T>();
            var result = await ExecuteAsync(statement, entity);
            return result;
        }

        public async Task<int> PutAsync(T entity)
        {
            string statement = MssqlQueryBuilder.BuildSinglePutStatement<T>();
            int result = await ExecuteAsync(statement, entity);
            return result;
        }

        public async Task<IEnumerable<V>> QueryAsync<V>(string query, object param = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    var result = await connection.QueryAsync<V>(query, param);
                    connection.Close();
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close();
                }

            }

        }
        public async Task<IEnumerable<V>> QuerySPAsync<V>(string storeProcedureName, object param = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    var result = await connection.QueryAsync<V>(storeProcedureName, param, null, null, CommandType.StoredProcedure);
                    connection.Close();
                    return result;
                }
                catch (Exception e)
                {
                    connection.Close();
                    throw e;
                }
            }

        }
        public async Task<int> ExecuteAsync(string query, object param = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    var result = await connection.ExecuteScalarAsync<int>(query, param);
                    connection.Close();
                    return result;
                }
                catch (Exception e)
                {
                    connection.Close();
                    throw;
                }

            }
        }
        public async Task<int> ExecuteSPAsync(string storeProcedureName, object param = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    var result = await connection.ExecuteAsync(storeProcedureName, param, null, null, CommandType.StoredProcedure);
                    connection.Close();
                    return result;
                }
                catch (Exception e)
                {
                    connection.Close();
                    throw e;
                }
            }

        }
        public async Task<Y> ExecuteScalarSPAsync<Y>(string storeProcedureName, object param = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    var result = await connection.ExecuteScalarAsync<Y>(storeProcedureName, param, null, null, CommandType.StoredProcedure);
                    connection.Close();
                    return result;
                }
                catch (Exception e)
                {
                    connection.Close();
                    throw e;
                }
            }

        }


        public async Task<long> GetSequenceValueAsync(string sequenceName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select next value for {0}";
                query = string.Format(query, sequenceName);
                var values = await connection.QueryAsync<long>(query);
                connection.Close();
                return values.ElementAt(0);
            }
        }
        #endregion

        #region sync methods
        public IEnumerable<T> Find(object condition)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string statement = MssqlQueryBuilder.BuildSelectStatement<T>(condition);
                var result = connection.Query<T>(statement, condition);
                connection.Close();
                return result;
            }
        }
        public T GetFirst(object condition)
        {
            string statement = MssqlQueryBuilder.BuildSelectStatement<T>(condition);
            var result = Query<T>(statement, condition);
            if (result.Any())
                return result.ElementAt(0);
            return null;
        }
        public int Insert(T entity)
        {
            string statement = MssqlQueryBuilder.BuildInsertStatement<T>();
            int result = Execute(statement, entity);
            return result;
        }
        public int Update(T entity)
        {
            string statement = MssqlQueryBuilder.BuildSingleUpdateStatement<T>();
            var result = Execute(statement, entity);
            return result;
        }
        public int Delete(T entity)
        {
            string statement = MssqlQueryBuilder.BuildSingleUpdateStatement<T>();
            int result = Execute(statement, entity);
            return result;
        }

        public int Put(T entity)
        {
            string statement = MssqlQueryBuilder.BuildSinglePutStatement<T>();
            int result = Execute(statement, entity);
            return result;
        }

        public IEnumerable<V> Query<V>(string query, object param = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var result = connection.Query<V>(query, param);
                    connection.Close();
                    return result;
                }
                catch (Exception)
                {
                    connection.Close();
                    throw;
                }
            }

        }
        public IEnumerable<V> QuerySP<V>(string storeProcedureName, object param = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    var result = connection.Query<V>(storeProcedureName, param, null, true, null, CommandType.StoredProcedure);
                    connection.Close();
                    return result;
                }
                catch (Exception)
                {
                    connection.Close();
                    throw;
                }
            }

        }
        public int Execute(string query, object param = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    var result = connection.Execute(query, param);
                    connection.Close();
                    return result;
                }
                catch (Exception)
                {
                    connection.Close();
                    throw;
                }
            }
        }
        public int ExecuteSP(string storeProcedureName, object param = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    var result = connection.Execute(storeProcedureName, param, null, null, CommandType.StoredProcedure);
                    connection.Close();
                    return result;
                }
                catch (Exception)
                {
                    connection.Close();
                    throw;
                }
            }

        }

        public long GetSequenceValue(string sequenceName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "select next value for {0}";
                    query = string.Format(query, sequenceName);
                    var values = connection.Query<long>(query);
                    connection.Close();
                    return values.ElementAt(0);
                }
                catch (Exception)
                {
                    connection.Close();
                    throw;
                }
            }
        }
        #endregion

        #region transaction
        public int InsertContinously(SqlConnection connection, SqlTransaction transaction, T entity)
        {
            string statement = MssqlQueryBuilder.BuildInsertStatement<T>();
            int result = ExecuteContinously(connection, transaction, statement, entity);
            return result;
        }
        public int UpdateContinously(SqlConnection connection, SqlTransaction transaction, T entity)
        {
            string statement = MssqlQueryBuilder.BuildSingleUpdateStatement<T>();
            var result = ExecuteContinously(connection, transaction, statement, entity);
            return result;
        }
        public int DeleteContinously(SqlConnection connection, SqlTransaction transaction, T entity)
        {
            string statement = MssqlQueryBuilder.BuildSingleUpdateStatement<T>();
            int result = ExecuteContinously(connection, transaction, statement, entity);
            return result;
        }
        public int ExecuteContinously(SqlConnection connection, SqlTransaction transaction, string query, object param = null)
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            try
            {
                var result = connection.ExecuteScalar<int>(query, param, transaction);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int ExecuteSPContinously(SqlConnection connection, SqlTransaction transaction, string storeProcedureName, object param = null)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            try
            {
                var result = connection.Execute(storeProcedureName, param, transaction, null, CommandType.StoredProcedure);
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public Y ExecuteScalarSPContinously<Y>(SqlConnection connection, SqlTransaction transaction, string storeProcedureName, object param = null)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            try
            {
                var result = connection.ExecuteScalar<Y>(storeProcedureName, param, transaction, null, CommandType.StoredProcedure);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion input sql connection

    }
    
}
