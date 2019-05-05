using Hinox.Data.Mongo;
using Hinox.Data.Mongo.Dal.Dao;
using Hoinx.PetHub.Manager.Data.Mongo.Dao.Interfaces;
using Hoinx.PetHub.Manager.Data.Mongo.Entities;

namespace Hoinx.PetHub.Manager.Data.Mongo.Dao
{
    public class MdPetBreedDao : BaseLongIdMongoDao<MdPetBreed>, IMdPetBreedDao
    {
        public MdPetBreedDao(IMongoDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
