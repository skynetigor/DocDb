using System;
using DocDb.Mongo.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DocDb.Mongo.Implementation.QueryProviders.LazyLoading
{
    class LazyLoadingIncludableQueryable<T> : AbstractIncludableQueryable<T>, ILazyLoadingIncludableQueryable<T> where T: class 
    {
        private IQueryProviderFromPipeline<T> queryProvider;
        private IServiceProvider ServiceProvider { get; }

        public LazyLoadingIncludableQueryable(ITypeInitializer typeInitializer, IServiceProvider serviceProvider, IMongoDatabase database) : base(typeInitializer, database)
        {
            this.ServiceProvider = serviceProvider;
        }

        protected override IQueryProviderFromPipeline<T> QueryProviderFromPipeline
        {
            get
            {
                if (queryProvider == null)
                {
                    queryProvider = this.ServiceProvider.GetService<LazyLoadingQueryProvider<T>>();
                }

                return queryProvider;
            }
        }
    }
}
