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
    public class DistrictMdEntityToModelConverter : ITypeConverter<MdDistrict, DistrictModel>
    {
        public DistrictModel Convert(
            MdDistrict source, 
            DistrictModel destination, 
            ResolutionContext context
            )
        {
            if (source == null)
                return null;
            destination = new DistrictModel()
            {
                Id = source.Id,
                Name = source.Name,
                Level = Enumeration.FromValue<DistrictLevel>(source.LevelId)
            };
            return destination;
        }
    }
}
