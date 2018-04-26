using System;
using System.Collections.Generic;
using System.Text;

namespace DocDb.Mongo.Extensions
{
    public static class MongoDocumentDbExtensions
    {
        public static MongoDbOptions UseMongoDb(this DocumentDbOptionsBuilder optionsBuilder, string connectionString, bool dropCollectionEachTime = false)
        {
            MongoDbOptions options = new MongoDbOptions(connectionString, dropCollectionEachTime);
            optionsBuilder.ChangeDatabaseOptions(options);

            return options;
        }
    }
}
