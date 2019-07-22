using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.Core.ValueObjects;
using BoardGame.RewardRolling.WebApp.Admin.Models.AdministrativeUnit;
using BoardGame.RewardRolling.WebApp.Admin.Models.Customer;
using BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit;

namespace BoardGame.RewardRolling.WebApp.Services.Interfaces
{
    public interface IAdministrativeUnitService
    {
        Task ImportExcelStandardAdministrativeUnit(
            List<ExcelStandardAdministrativeUnitModel> excelModels
        );

        Task ImportExcelNhanhAdministrativeUnit(
            List<ExcelNhanhAdministrativeUnitModel> excelModels
        );

        Task ResetStandard();
        Task<List<CityModel>> GetAllCityAsync();
        Task<List<DistrictModel>> GetDistrictByCityIdAsync(string cityId);
        Task<List<CommuneModel>> GetCommuneByDistrictId(string districtId);
        Task<CityModel> AddCityAsync(AddCityRequest model);
        Task<CityModel> UpdateCity(string id, UpdateCityRequest model);
        Task DeleteCityAsync(string id);
        Task<DistrictModel> AddDistrict(AddDistrictRequest model);
        Task<CommuneModel> AddCommune(AddCommuneRequest model);
        Task<DistrictModel> UpdateDistrict(string id, UpdateDistrictRequest model);
        Task DeleteDistrictAsync(string id);
        Task<CommuneModel> UpdateCommune(string id, UpdateCommuneRequest model);
        Task DeleteCommuneAsync(string id);
        Task<ExcelCustomerAddress> MapAddressToExcelAddress(Address address);
    }
}
