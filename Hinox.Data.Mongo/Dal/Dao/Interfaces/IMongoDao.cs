using Hinox.Data.Mongo.Dal.Entities;
using Hinox.Data.Mongo.Filters;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hinox.Data.Mongo.Dal.Dao.Interfaces
{
    public interface IMongoDao<T, TId> where T : IMongoEntity<TId>
    {
        Task<T> GetByIdAsync(TId id);
        Task AddAsync(T dto);
        Task AddAsync(IEnumerable<T> listDto);
        Task<T> UpdateAsync(T dto);
        Task<T> DeleteAsync(T dto);
        Task<List<T>> Filter(BaseMdPagingFilter<T> mdFilter);
        Task<int> Count(BaseMdPagingFilter<T> mdFilter);
    }
    
}
