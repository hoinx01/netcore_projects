using AutoMapper;
using BoardGame.RewardRolling.Core.Auth;
using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.Data.Mongo.Entities;
using BoardGame.RewardRolling.Service.Domains;
using BoardGame.RewardRolling.WebApp.Models.AdministrativeUnit;
using BoardGame.RewardRolling.WebApp.Registrations.ObjectConverters;
using Hinox.Static.Enumerate;
using MongoDB.Bson;

namespace BoardGame.RewardRolling.WebApp.Registrations
{
    public class MapperInitiator
    {
        public static void Init()
        {
            Mapper.Initialize((cfg) =>
            {
                cfg.CreateMissingTypeMaps = true;
                cfg.ForAllMaps((map, exp) => {
                    foreach (var unmappedPropertyName in map.GetUnmappedPropertyNames())
                    {
                        exp.ForMember(unmappedPropertyName, opt => opt.Ignore());
                    }
                });
                
                RegisterMapping(cfg);
            });
           
        }
        /*
         * Cần custom mapping nào thì viết ở đây
         */
        public static void RegisterMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Enumeration, string>().ConvertUsing<EnumerationNameTypeConverter>();
            cfg.CreateMap<Enumeration, int>().ConvertUsing<EnumerationIdTypeConverter>();
            cfg.CreateMap<string, ObjectId>().ConvertUsing<StringBsonIdTypeConverter>();
            cfg.CreateMap<ObjectId, string>().ConvertUsing<BsonIdStringTypeConverter>();
            cfg.CreateMap<RewardDomain, MdReward>().ConvertUsing<RewardDomainToMdDtoConverter>();
            cfg.CreateMap<MdReward, RewardDomain>().ConvertUsing<RewardMdDtoToDomainConverter>();
            cfg.CreateMap<int, CampaignStatus>().ConvertUsing<CampaignStatusIdToEnumerationConverter>();
            cfg.CreateMap<ApplicationUser, MdUser>().ConvertUsing<UserIdentityToMdEntityConverter>();
            cfg.CreateMap<MdUser, ApplicationUser>().ConvertUsing<UserMdEntityToIdentityConverter>();
            cfg.CreateMap<MdCity, CityModel>().ConvertUsing<CityMdEntityToModelConverter>();
            cfg.CreateMap<MdDistrict, DistrictModel>().ConvertUsing<DistrictMdEntityToModelConverter>();
            cfg.CreateMap<MdCommune, CommuneModel>().ConvertUsing<CommuneMdEntityToModelConverter>();
        }
    }

    public class EnumerationNameTypeConverter : ITypeConverter<Enumeration, string>
    {
        public string Convert(Enumeration source, string destination, ResolutionContext context)
        {
            return source.Name;
        }
    }
    public class EnumerationIdTypeConverter : ITypeConverter<Enumeration, int>
    {
        public int Convert(Enumeration source, int destination, ResolutionContext context)
        {
            return source.Id;
        }
    }

    public class StringBsonIdTypeConverter : ITypeConverter<string, ObjectId>
    {
        public ObjectId Convert(string source, ObjectId destination, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source))
                return ObjectId.GenerateNewId();
            return new ObjectId(source);
        }
    }
    public class BsonIdStringTypeConverter : ITypeConverter<ObjectId, string>
    {
        public string Convert(ObjectId source, string destination, ResolutionContext context)
        {
            return source.ToString();
        }
    }

    public class RewardDomainToMdDtoConverter : ITypeConverter<RewardDomain, MdReward>
    {
        public MdReward Convert(RewardDomain source, MdReward destination, ResolutionContext context)
        {
            destination = new MdReward()
            {
                Name = source.Name,
                Cost = source.Cost,
                CreatedAt = source.CreatedAt,
                ModifiedAt = source.ModifiedAt,
                Status = source.Status.Id
            };
            if (string.IsNullOrWhiteSpace(source.Id))
                destination.Id = ObjectId.GenerateNewId();
            else
                destination.Id = new ObjectId(source.Id);

            return destination;
        }
    }
    public class RewardMdDtoToDomainConverter : ITypeConverter<MdReward, RewardDomain>
    {
        public RewardDomain Convert(MdReward source, RewardDomain destination, ResolutionContext context)
        {
            destination = new RewardDomain()
            {
                Id = source.Id.ToString(),
                Name = source.Name,
                Cost = source.Cost,
                CreatedAt = source.CreatedAt,
                ModifiedAt = source.ModifiedAt,
                Status = Enumeration.FromValue<RewardStatus>(source.Status)
            };
            return destination;
        }
    }

}
