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


    }
    public interface IMdDistrictDao : IStringIdMongoDao<MdDistrict>
    {
        Task<List<MdDistrict>> GetAllAsync();
        Task<List<MdDistrict>> GetByCityIdAsync(string cityId);
    }

    public interface IMdCommuneDao : IStringIdMongoDao<MdCommune>
    {
        Task<List<MdCommune>> GetAllAsync();
        Task<List<MdCommune>> GetByDistrictIdAsync(string districtId);
    }
}
