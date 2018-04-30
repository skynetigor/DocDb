using DocDb.Core;

namespace DocDb.Mongo.Extensions
{
    public static class MongoDocumentDbExtensions
    {
        public static MongoDbOptions UseMongoDb(this DocumentDbOptionsBuilder optionsBuilder, string connectionString, bool dropCollectionEachTime = false)
        {
            MongoDbOptions options = new MongoDbOptions(connectionString, dropCollectionEachTime);
            optionsBuilder.UseOptions(options);

            return options;
        }
    }
}
