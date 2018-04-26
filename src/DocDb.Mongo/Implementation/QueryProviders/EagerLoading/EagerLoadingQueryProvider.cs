using DocDb.Mongo.Abstracts;
using MongoDB.Driver;

namespace DocDb.Mongo.Implementation.QueryProviders.EagerLoading
{
    internal class EagerLoadingQueryProvider<T>: AbstractQueryProviderFromPipeline<T>
    {
        public EagerLoadingQueryProvider(IMongoDatabase database, ITypeInitializer typeInitializer) : base(database, typeInitializer)
        {
        }
    }
}
