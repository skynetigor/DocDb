using System;
using DocDb.Mongo.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DocDb.Mongo.Implementation.QueryProviders.EagerLoading
{
    class EagerLoadingIncludableQueryable<T> : AbstractIncludableQueryable<T>, IEagerLoadingIncludableQueryable<T> where T: class
    {
        private IQueryProviderFromPipeline<T> queryProvider;

        private IServiceProvider ServiceProvider { get; }

        protected override IQueryProviderFromPipeline<T> QueryProviderFromPipeline => queryProvider ?? (queryProvider = this.ServiceProvider.GetService<EagerLoadingQueryProvider<T>>());

        public EagerLoadingIncludableQueryable(ITypeInitializer typeInitializer, IServiceProvider serviceProvider, IMongoDatabase database) : base(typeInitializer, database)
        {
            this.ServiceProvider = serviceProvider;
        }
    }
}
