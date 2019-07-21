using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using Hinox.Data.Mongo.Dal.Dao.Interfaces;

namespace BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces
{
    public interface IMdCityDao : IStringIdMongoDao<MdCity>
    {
        Task<List<MdCity>> GetAllAsync();
        Task<string> GetNextId();

    }
    public interface IMdDistrictDao : IStringIdMongoDao<MdDistrict>
    {
        Task<List<MdDistrict>> GetAllAsync();
        Task<List<MdDistrict>> GetByCityIdAsync(string cityId);
        Task<string> GetNextId();
    }

    public interface IMdCommuneDao : IStringIdMongoDao<MdCommune>
    {
        Task<List<MdCommune>> GetAllAsync();
        Task<List<MdCommune>> GetByDistrictIdAsync(string districtId);
        Task<string> GetNextId();
    }
}
