using Data.Mongo.Dao.Interfaces;
using Data.Mongo.Entities;
using Data.Mongo.Filters;
using Hinox.Data.Mongo;
using Hinox.Data.Mongo.Dal.Dao;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mongo.Dao
{
    public class MdPetSpeciesDao : BaseLongIdMongoDao<MdPetSpecies>, IMdPetSpeciesDao
    {
        public MdPetSpeciesDao(IMongoDbFactory mongoDbFactory) : base(mongoDbFactory)
        {
            
        }
    }
}
