using Data.Mongo.Entities;
using Data.Mongo.Filters;
using Hinox.Data.Mongo.Dal.Dao.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Mongo.Dao.Interfaces
{
    public interface IMdPetSpeciesDao : ILongIdMongoDao<MdPetSpecies>
    {
    }
}
