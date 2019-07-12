using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit;
using Hinox.Static.Enumerate;

namespace BoardGame.RewardRolling.WebApp.Registrations.ObjectConverters
{
    public class CityMdEntityToModelConverter : ITypeConverter<MdCity, CityModel>
    {
        public CityModel Convert(MdCity source, CityModel destination, ResolutionContext context)
        {
            if (source == null)
                return null;
            destination = new CityModel()
            {
                Id = source.Id,
                Name = source.Name,
                Level = Enumeration.FromValue<CityLevel>(source.LevelId)
            };
            return destination;
        }
    }
}
