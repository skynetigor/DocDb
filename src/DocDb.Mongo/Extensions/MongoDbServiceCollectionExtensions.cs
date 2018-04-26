using System.Linq;
using DocDb.Core;
using DocDb.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DocDb.Mongo.Extensions
{
    static class MongoDbServiceCollectionExtensions
    {
        public static IServiceCollection UseMongoDbScoped<TContext>(IServiceCollection serviceCollection, string connectionString) where TContext :DocumentDbContext
        {
            serviceCollection.UseDocumentDbContext<TContext>(new MongoDbOptions(connectionString),
                ServiceLifetime.Scoped);
            return serviceCollection;
        }
    }
}
