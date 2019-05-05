using Data.Mongo.Dao.Interfaces;
using Data.Mongo.Entities;
using Hinox.Data.Mongo;
using Hinox.Data.Mongo.Dal.Dao;

namespace Data.Mongo.Dao
{
    public class MdPetBreedDao : BaseLongIdMongoDao<MdPetBreed>, IMdPetBreedDao
    {
        public MdPetBreedDao(IMongoDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
