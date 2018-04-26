using DocDb.Core.Abstracts;
using DocDb.Core.Extensions;
using DocDb.Mongo.Abstracts;
using DocDb.Mongo.Extensions;

namespace DocDb.Mongo
{
    public class MongoDbOptions: IDocumentDbOptions
    {
        private IDocumentDbDataProvider _providerInstance;

        public IDocumentDbDataProvider ProviderInstance
        {
            get
            {
                if (_providerInstance.IsNull())
                {
                    _providerInstance = new MongoDbProvider(this);
                }

                return _providerInstance;
            }
        }

        public MongoDbOptions(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public MongoDbOptions(string connectionString, bool dropCollectionsEachTime) : this(connectionString)
        {
            DropCollectionsEachTime = dropCollectionsEachTime;
        }

        public string ConnectionString { get; }

        public bool DropCollectionsEachTime { get; }
    }
}
