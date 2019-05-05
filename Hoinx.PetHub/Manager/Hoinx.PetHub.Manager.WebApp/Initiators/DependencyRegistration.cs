using Hinox.Data.Mongo;
using Hoinx.PetHub.Manager.Data.Mongo;
using Hoinx.PetHub.Manager.Data.Mongo.Dao;
using Hoinx.PetHub.Manager.Data.Mongo.Dao.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Hoinx.PetHub.Manager.WebApp.Initiators
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
