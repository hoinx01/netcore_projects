using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Admin.Models.AdministrativeUnit;
using BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit;

namespace BoardGame.RewardRolling.WebApp.Services.Interfaces
{
    public interface IAdministrativeUnitService
    {
        Task ImportExcelAdministrativeUnit(
            List<ExcelStandardAdministrativeUnitModel> excelModels
        );

        Task Reset();
        Task<List<CityModel>> GetAllCityAsync();
        Task<List<DistrictModel>> GetDistrictByCityIdAsync(string cityId);
        Task<List<CommuneModel>> GetCommuneByDistrictId(string districtId);
    }
}
