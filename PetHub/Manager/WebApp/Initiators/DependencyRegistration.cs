using Data.Mongo;
using Data.Mongo.Dao;
using Data.Mongo.Dao.Interfaces;
using Hinox.Data.Mongo;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Initiators
{
    public static class DependencyRegistration
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<IMongoDbFactory, MongoDbFactory>();
            services.AddTransient<IMdPetSpeciesDao, MdPetSpeciesDao>();
            services.AddTransient<IMdPetBreedDao, MdPetBreedDao>();
        }
    }
}
