using Hinox.Data.Mongo;
using Hinox.Data.Mongo.Attributes;
using Hinox.Data.Mongo.Configurations;
using Hinox.Static.Application;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.RewardRolling.Data.Mongo
{
    public class MongoDbFactory : IMongoDbFactory
    {
        public static Dictionary<string, MongoConnection> MongoConnections = AppSettings.Get<Dictionary<string, MongoConnection>>("Databases:Mongo:Connections");

        public IMongoDatabase GetMongoDatabase<T>()
        {
            IMongoDatabase database = null;
            var mongoConnection = MongoConnections["Main"];
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(mongoConnection.ConnectionString));
                if (mongoConnection.Ssl)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                database = mongoClient.GetDatabase(mongoConnection.DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access MongoDb server.", ex);
            }
            return database;
        }
        public string GetCollectionName<T>()
        {
            string collectionName = null;
            var type = typeof(T);
            var collectionAttributes = type.GetCustomAttributes(typeof(MdCollectionAttribute), false);
            if (collectionAttributes != null && collectionAttributes.Length == 1)
            {
                collectionName = ((MdCollectionAttribute)collectionAttributes.GetValue(0)).Name;
                return collectionName;
            }
            throw new Exception("Cannot determine collectionName for type: " + type.Name);
        }
    }
}
