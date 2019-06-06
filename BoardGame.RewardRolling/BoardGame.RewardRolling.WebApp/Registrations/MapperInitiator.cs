using AutoMapper;
using BoardGame.RewardRolling.Core.Statics;
using Hinox.Static.Enumerate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Registrations
{
    public class MapperInitiator
    {
        public static void Init()
        {
            Mapper.Initialize((cfg) => {
                cfg.CreateMissingTypeMaps = true;
                cfg.ForAllMaps((map, exp) => {
                    foreach (var unmappedPropertyName in map.GetUnmappedPropertyNames())
                    {
                        exp.ForMember(unmappedPropertyName, opt => opt.Ignore());
                    }
                });
                cfg.CreateMap<Enumeration, string>().ConvertUsing<EnumerationNameTypeConverter>();
                cfg.CreateMap<Enumeration, int>().ConvertUsing<EnumerationIdTypeConverter>();
                cfg.CreateMap<string, ObjectId>().ConvertUsing<StringBsonIdTypeConverter>();
                cfg.CreateMap<ObjectId, string>().ConvertUsing<BsonIdStringTypeConverter>();
            });
            RegisterMapping();
        }
        /*
         * Cần custom mapping nào thì viết ở đây
         */
        public static void RegisterMapping()
        {
            return;
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
}
