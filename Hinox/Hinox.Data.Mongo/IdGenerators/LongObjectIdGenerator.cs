using Hinox.Static.Application;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Hinox.Data.Mongo.Dal.Entities;
using Hinox.Data.Mongo.Attributes;

namespace Hinox.Data.Mongo.IdGenerators
{
    public class LongObjectIdGenerator : IIdGenerator
    {
        private readonly IMongoDbFactory mongoDbFactory;
        private IMongoCollection<MdLongSequenceId> longSequenceIdCollection;
        public LongObjectIdGenerator(
            )
        {
            this.mongoDbFactory = (IMongoDbFactory) DependencyManager.ServiceProvider.GetService(typeof(IMongoDbFactory));
            longSequenceIdCollection = mongoDbFactory
                .GetMongoDatabase<MdLongSequenceId>()
                .GetCollection<MdLongSequenceId>(mongoDbFactory.GetCollectionName<MdLongSequenceId>());
        }
        //
        // Summary:
        //     Generates an Id for a document.
        //
        // Parameters:
        //   container:
        //     The container of the document (will be a MongoCollection when called from the
        //     C# driver).
        //
        //   document:
        //     The document.
        //
        // Returns:
        //     An Id.
        public object GenerateId(object container, object document)
        {
            var documentType = document.GetType();

            string sequenceIdInstanceId = null;
            var collectionAttributes = documentType.GetCustomAttributes(typeof(MdSequenceId), false);
            if (collectionAttributes != null && collectionAttributes.Length == 1)
            {
                sequenceIdInstanceId = ((MdSequenceId)collectionAttributes.GetValue(0)).Id;
            }

            var filter = Builders<MdLongSequenceId>.Filter.Eq("_id", sequenceIdInstanceId);

            var updateDefinition = Builders<MdLongSequenceId>.Update.Inc("CurrentValue", 1);
            var longSequenceId = longSequenceIdCollection.FindOneAndUpdate(
                    filter,
                    updateDefinition,
                    new FindOneAndUpdateOptions<MdLongSequenceId, MdLongSequenceId>()
                    {
                        IsUpsert = true,
                        ReturnDocument = ReturnDocument.After
                    }
                );
            return longSequenceId.CurrentValue;
        }
        //
        // Summary:
        //     Tests whether an Id is empty.
        //
        // Parameters:
        //   id:
        //     The Id.
        //
        // Returns:
        //     True if the Id is empty.
        public bool IsEmpty(object id)
        {
            if ((long)id == 0)
                return true;
            return false;
        }
    }
}
