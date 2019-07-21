using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Data.Mongo.Dao;
using BoardGame.RewardRolling.Data.Mongo.Dao.Interfaces;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using BoardGame.RewardRolling.WebApp.Admin.Models.AdministrativeUnit;
using BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Mvc.Exceptions;
using Hinox.Static.Enumerate;
using Hinox.Static.Extensions;
using MongoDB.Driver;

namespace BoardGame.RewardRolling.WebApp.Services
{
    public class AdministrativeUnitService : IAdministrativeUnitService
    {
        private readonly IMdCityDao cityDao;
        private readonly IMdDistrictDao districtDao;
        private readonly IMdCommuneDao communeDao;
        private readonly IMdNhanhCityDao nhanhCityDao;
        private readonly IMdStandardToNhanhAdministrativeUnitMapDao mapDao;

        public AdministrativeUnitService(
            IMdCityDao cityDao,
            IMdDistrictDao districtDao,
            IMdCommuneDao communeDao,
            IMdNhanhCityDao nhanhCityDao,
            IMdStandardToNhanhAdministrativeUnitMapDao mapDao
        )
        {
            this.cityDao = cityDao;
            this.districtDao = districtDao;
            this.communeDao = communeDao;
            this.nhanhCityDao = nhanhCityDao;
            this.mapDao = mapDao;
        }

        public async Task ResetStandard()
        {
            await cityDao.Collection.DeleteManyAsync(FilterDefinition<MdCity>.Empty);
            await districtDao.Collection.DeleteManyAsync(FilterDefinition<MdDistrict>.Empty);
            await communeDao.Collection.DeleteManyAsync(FilterDefinition<MdCommune>.Empty);
        }

        public async Task ImportExcelStandardAdministrativeUnit(
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

        public async Task ImportExcelNhanhAdministrativeUnit(
            List<ExcelNhanhAdministrativeUnitModel> excelModels
            )
        {
            await nhanhCityDao.Collection.DeleteManyAsync(FilterDefinition<MdNhanhCity>.Empty);
            await mapDao.Collection.DeleteManyAsync(FilterDefinition<MdStandardToNhanhAdministrativeUnitMap>.Empty);

            var cityModels = new List<NhanhCityModel>();

            foreach (var excelModel in excelModels)
            {
                var cityModel = cityModels.FirstOrDefault(f => f.Id == excelModel.CityId);
                if (cityModel == null)
                {
                    cityModel = new NhanhCityModel()
                    {
                        Id = excelModel.CityId,
                        Name = excelModel.CityName,
                        Districts = new List<NhanhDistrictModel>()
                    };
                    cityModels.Add(cityModel);
                }

                var districtModel = cityModel.Districts.FirstOrDefault(
                    f => f.Id == excelModel.DistrictId
                         );
                if (districtModel == null)
                {
                    districtModel = new NhanhDistrictModel()
                    {
                        Id = excelModel.DistrictId,
                        Name = excelModel.DistrictName
                    };

                    cityModel.Districts.Add(districtModel);
                }
            }

            var mdNhanhCities = cityModels.Select(s => Mapper.Map<MdNhanhCity>(s)).ToList();
            await nhanhCityDao.AddAsync(mdNhanhCities);
            await MapStandardVsNhanhAdministrativeUnit();
        }

        public async Task MapStandardVsNhanhAdministrativeUnit()
        {
            var standardCities = await cityDao.GetAllAsync();
            var standardDistricts = await districtDao.GetAllAsync();

            var nhanhCities = await nhanhCityDao.GetAllAsync();
            var nhanhCityNames = nhanhCities.Select(s => s.Name.ToLower()
                .Replace("tỉnh","")
                .Replace("thành phố", "")
                )
                .ToList();

            var maps = new List<MdStandardToNhanhAdministrativeUnitMap>();

            foreach (var standardCity in standardCities)
            {
                int nhanhCityIndex = -1;
                try
                {
                    nhanhCityIndex = standardCity.Name.ToLower()
                        .Replace("tỉnh", "")
                        .Replace("thành phố", "").GetClosestText(nhanhCityNames, 1);
                }
                catch (Exception e)
                {
                    continue;
                }
                var nhanhCity = nhanhCities[nhanhCityIndex];
                var nhanhDistricts = nhanhCity.Districts;
                var nhanhDistrictNames = nhanhDistricts.Select(s => s.Name.ToLower()
                    .Replace("quận", "")
                    .Replace("thành phố", "")
                    .Replace("huyện", "")
                    .Replace("thị xã", "")
                ).ToList();

                var currentCityStandardDistricts = standardDistricts.Where(w => w.CityId == standardCity.Id).ToList();
                foreach (var currentCidyStandardDistrict in currentCityStandardDistricts)
                {
                    int nhanhDistrictIndex = -1;
                    try
                    {
                        nhanhDistrictIndex = currentCidyStandardDistrict.Name.ToLower()
                            .Replace("quận", "")
                            .Replace("thành phố", "")
                            .Replace("huyện", "")
                            .Replace("thị xã", "")
                            .GetClosestText(nhanhDistrictNames, 2);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                    var nhanhDistrict = nhanhDistricts[nhanhDistrictIndex];

                    var map = new MdStandardToNhanhAdministrativeUnitMap()
                    {
                        StandardCityId = standardCity.Id,
                        StandardDistrictId = currentCidyStandardDistrict.Id,
                        NhanhCityId = nhanhCity.Id,
                        NhanhDistrictId = nhanhDistrict.Id
                    };
                    maps.Add(map);
                }
            }

            await mapDao.AddAsync(maps);
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

        public async Task<CityModel> AddCityAsync(AddCityRequest model)
        {
            var mdCity = new MdCity()
            {
                Name = model.Name,
                LevelId = model.LevelId.Value
            };
            var id = await cityDao.GetNextId();
            mdCity.Id = id;

            await cityDao.AddAsync(mdCity);
            var result = Mapper.Map<CityModel>(mdCity);
            return result;
        }

        public async Task<CityModel> UpdateCity(string id, UpdateCityRequest model)
        {
            var mdCity = await cityDao.GetByIdAsync(id);

            if(mdCity == null)
                throw new NotFoundException();

            mdCity.Name = model.Name;
            mdCity.LevelId = model.LevelId.Value;

            await cityDao.UpdateAsync(mdCity);
            var result = Mapper.Map<CityModel>(mdCity);
            return result;
        }

        public async Task DeleteCityAsync(string id)
        {
            var mdCity = await cityDao.GetByIdAsync(id);

            if (mdCity == null)
                throw new NotFoundException();

            await cityDao.DeleteAsync(mdCity);
        }

        public async Task<DistrictModel> AddDistrict(AddDistrictRequest model)
        {
            var mdDistrict = new MdDistrict()
            {
                CityId = model.CityId,
                Name = model.Name,
                LevelId = model.LevelId.Value
            };
            var id = await districtDao.GetNextId();
            mdDistrict.Id = id;

            await districtDao.AddAsync(mdDistrict);
            var result = Mapper.Map<DistrictModel>(mdDistrict);
            return result;
        }

        public async Task<DistrictModel> UpdateDistrict(string id, UpdateDistrictRequest model)
        {
            var mdDistrict = await districtDao.GetByIdAsync(id);

            if (mdDistrict == null)
                throw new NotFoundException();

            mdDistrict.Name = model.Name;
            mdDistrict.LevelId = model.LevelId.Value;

            await districtDao.UpdateAsync(mdDistrict);
            var result = Mapper.Map<DistrictModel>(mdDistrict);
            return result;
        }

        public async Task DeleteDistrictAsync(string id)
        {
            var mdDistrict = await districtDao.GetByIdAsync(id);

            if (mdDistrict == null)
                throw new NotFoundException();

            await districtDao.DeleteAsync(mdDistrict);
        }
        public async Task<CommuneModel> AddCommune(AddCommuneRequest model)
        {
            var mdCommune = new MdCommune()
            {
                DistrictId = model.DistrictId,
                CityId = model.CityId,
                Name = model.Name,
                LevelId = model.LevelId.Value
            };
            var id = await districtDao.GetNextId();
            mdCommune.Id = id;

            await communeDao.AddAsync(mdCommune);
            var result = Mapper.Map<CommuneModel>(mdCommune);
            return result;
        }

        public async Task<CommuneModel> UpdateCommune(string id, UpdateCommuneRequest model)
        {
            var mdCommune = await communeDao.GetByIdAsync(id);

            if (mdCommune == null)
                throw new NotFoundException();

            mdCommune.Name = model.Name;
            mdCommune.LevelId = model.LevelId.Value;

            await communeDao.UpdateAsync(mdCommune);
            var result = Mapper.Map<CommuneModel>(mdCommune);
            return result;
        }

        public async Task DeleteCommuneAsync(string id)
        {
            var mdCommune = await communeDao.GetByIdAsync(id);

            if (mdCommune == null)
                throw new NotFoundException();

            await communeDao.DeleteAsync(mdCommune);
        }
    }
}
