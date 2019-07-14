using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using BoardGame.RewardRolling.WebApp.Admin.Models.AdministrativeUnit;
using BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Static.Enumerate;
using MongoDB.Driver;

namespace BoardGame.RewardRolling.WebApp.Services
{
    public class AdministrativeUnitService : IAdministrativeUnitService
    {
        private readonly IMdCityDao cityDao;
        private readonly IMdDistrictDao districtDao;
        private readonly IMdCommuneDao communeDao;

        public AdministrativeUnitService(
            IMdCityDao cityDao,
            IMdDistrictDao districtDao,
            IMdCommuneDao communeDao
        )
        {
            this.cityDao = cityDao;
            this.districtDao = districtDao;
            this.communeDao = communeDao;
        }

        public async Task Reset()
        {
            await cityDao.Collection.DeleteManyAsync(FilterDefinition<MdCity>.Empty);
            await districtDao.Collection.DeleteManyAsync(FilterDefinition<MdDistrict>.Empty);
            await communeDao.Collection.DeleteManyAsync(FilterDefinition<MdCommune>.Empty);
        }

        public async Task ImportExcelAdministrativeUnit(
            List<ExcelStandardAdministrativeUnitModel> excelModels
            )
        {
            var cityModels = new List<CityModel>();

            foreach (var excelModel in excelModels)
            {
                var cityModel = cityModels.FirstOrDefault(f => f.Id == excelModel.CityId);
                if (cityModel == null)
                {
                    cityModel = new CityModel()
                    {
                        Id = excelModel.CityId,
                        Name = excelModel.CityName,
                        Districts = new List<DistrictModel>()
                    };
                    if(excelModel.CityName.ToLower().StartsWith("thành phố"))
                        cityModel.Level = CityLevel.City;
                    else if(excelModel.CityName.ToLower().StartsWith("tỉnh"))
                        cityModel.Level = CityLevel.Province;
                    cityModels.Add(cityModel);
                }

                var districtModel = cityModel.Districts.FirstOrDefault(
                    f => f.Id == excelModel.DistrictId
                         );
                if (districtModel == null)
                {
                    districtModel = new DistrictModel()
                    {
                        Id = excelModel.DistrictId,
                        Name = excelModel.DistrictName,
                        Communes = new List<CommuneModel>()
                    };

                    if(excelModel.DistrictName.ToLower().StartsWith("quận"))
                        districtModel.Level = DistrictLevel.District;
                    else if (excelModel.DistrictName.ToLower().StartsWith("thị xã"))
                        districtModel.Level = DistrictLevel.Borough;
                    else if (excelModel.DistrictName.ToLower().StartsWith("huyện"))
                        districtModel.Level = DistrictLevel.Town;
                    else if (excelModel.DistrictName.ToLower().StartsWith("thành phố"))
                        districtModel.Level = DistrictLevel.SubCity;

                    cityModel.Districts.Add(districtModel);
                }

                var communeModel = new CommuneModel()
                {
                    Id = excelModel.CommuneId,
                    Name =  excelModel.CommuneName
                };
                if(excelModel.CommuneName.ToLower().StartsWith("phường"))
                    communeModel.Level = CommuneLevel.Ward;
                else if (excelModel.CommuneName.ToLower().StartsWith("thị trấn"))
                    communeModel.Level = CommuneLevel.HighCommune;
                else if (excelModel.CommuneName.ToLower().StartsWith("xã"))
                    communeModel.Level = CommuneLevel.Commune;

                districtModel.Communes.Add(communeModel);
            }

            var mdCities = new List<MdCity>();
            var mdDistricts = new List<MdDistrict>();
            var mdCommunes = new List<MdCommune>();

            foreach (var cityModel in cityModels)
            {
                var mdCity = Mapper.Map<MdCity>(cityModel);
                var currentMdDistricts = cityModel.Districts.Select(
                    s => Mapper.Map<MdDistrict>(s)
                    ).ToList();
                foreach (var currentMdDistrict in currentMdDistricts)
                {
                    currentMdDistrict.CityId = mdCity.Id;
                }
                mdDistricts.AddRange(currentMdDistricts);

                foreach (var districtModel in cityModel.Districts)
                {
                    var currentMdCommunes = districtModel.Communes.Select(
                        s => Mapper.Map<MdCommune>(s)
                        ).ToList();
                    foreach (var currentMdCommune in currentMdCommunes)
                    {
                        currentMdCommune.CityId = cityModel.Id;
                        currentMdCommune.DistrictId = districtModel.Id;
                        mdCommunes.Add(currentMdCommune);
                    }
                }
                mdCities.Add(mdCity);
            }

            await cityDao.AddAsync(mdCities);
            await districtDao.AddAsync(mdDistricts);
            await communeDao.AddAsync(mdCommunes);
        }

        public async Task<List<CityModel>> GetAllCityAsync()
        {
            var mdCities = await cityDao.GetAllAsync();
            var cityModels = mdCities.Select(s => Mapper.Map<CityModel>(s)).ToList();
            return cityModels;
        }
        public async Task<List<DistrictModel>> GetDistrictByCityIdAsync(string cityId)
        {
            var mdDistricts = await districtDao.GetByCityIdAsync(cityId);
            var districtModels = mdDistricts.Select(s => Mapper.Map<DistrictModel>(s)).ToList();
            return districtModels;
        }

        public async Task<List<CommuneModel>> GetCommuneByDistrictId(string districtId)
        {
            var mdCommunes = await communeDao.GetByDistrictIdAsync(districtId);
            var communeModels = mdCommunes.Select(s => Mapper.Map<CommuneModel>(s)).ToList();
            return communeModels;
        }
    }
}
