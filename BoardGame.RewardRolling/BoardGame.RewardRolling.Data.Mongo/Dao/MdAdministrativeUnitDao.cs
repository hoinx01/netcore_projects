using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo;
using Hinox.Data.Mongo.Dal.Dao;
using MongoDB.Driver;

namespace BoardGame.RewardRolling.Data.Mongo.Dao
{
    public class MdCityDao : BaseStringIdMongoDao<MdCity>, IMdCityDao
    {
        public MdCityDao(IMongoDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<List<MdCity>> GetAllAsync()
        {
            var filterDefinition = Builders<MdCity>.Filter.Empty;
            var filterResult = await Collection.FindAsync(filterDefinition);
            var entities = await filterResult.ToListAsync();
            return entities;
        }
    }
    public class MdDistrictDao : BaseStringIdMongoDao<MdDistrict>, IMdDistrictDao
    {
        public MdDistrictDao(IMongoDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<List<MdDistrict>> GetAllAsync()
        {
            var filterDefinition = Builders<MdDistrict>.Filter.Empty;
            var filterResult = await Collection.FindAsync(filterDefinition);
            var entities = await filterResult.ToListAsync();
            return entities;
        }
        public async Task<List<MdDistrict>> GetByCityIdAsync(string cityId)
        {
            var filterDefinition = Builders<MdDistrict>
                .Filter
                .Eq(nameof(MdDistrict.CityId), cityId);
            var filterResult = await Collection.FindAsync(filterDefinition);
            var entities = await filterResult.ToListAsync();
            return entities;
        }
    }
    public class MdCommuneDao : BaseStringIdMongoDao<MdCommune>, IMdCommuneDao
    {
        public MdCommuneDao(IMongoDbFactory dbFactory) : base(dbFactory)
        {

        }
        public async Task<List<MdCommune>> GetAllAsync()
        {
            var filterDefinition = Builders<MdCommune>.Filter.Empty;
            var filterResult = await Collection.FindAsync(filterDefinition);
            var entities = await filterResult.ToListAsync();
            return entities;
        }

        public async Task<List<MdCommune>> GetByDistrictIdAsync(string districtId)
        {
            var filterDefinition = Builders<MdCommune>
                .Filter
                .Eq(nameof(MdCommune.DistrictId), districtId);
            var filterResult = await Collection.FindAsync(filterDefinition);
            var entities = await filterResult.ToListAsync();
            return entities;
        }
    }
}
