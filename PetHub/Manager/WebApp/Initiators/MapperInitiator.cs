using AutoMapper;
using Hinox.Static.Enumerate;

namespace WebApp.Initiators
{
    public static class MapperInitiator
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
}
