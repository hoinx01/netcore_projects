using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Hinox.Data.Mssql
{
    public interface IDatabaseFactory
    {
        string GetConnectionString<T>();
        SqlConnection GetConnection<T>();
        SqlConnection GetConnection(string key = null);
        DbContext GetDbContext();
    }
}
