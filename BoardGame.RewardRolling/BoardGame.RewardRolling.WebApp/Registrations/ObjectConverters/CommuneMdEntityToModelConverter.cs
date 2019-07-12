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
    public class CommuneMdEntityToModelConverter : ITypeConverter<MdCommune, CommuneModel>
    {
        public CommuneModel Convert(MdCommune source, CommuneModel destination, ResolutionContext context)
        {
            if (source == null)
                return null;
            destination = new CommuneModel()
            {
                Id = source.Id,
                Name = source.Name,
                Level = Enumeration.FromValue<CommuneLevel>(source.LevelId)
            };
            return destination;
        }
    }
}
