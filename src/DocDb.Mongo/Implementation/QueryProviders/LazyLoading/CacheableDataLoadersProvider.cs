using System;
using System.Collections.Generic;
using System.Linq;
using DocDb.Core.DI.Extensions;
using DocDb.Mongo.Abstracts;
using DocDb.Mongo.Extensions;
using DocDb.Mongo.Implementation.QueryProviders.LazyLoading.Loaders;

namespace DocDb.Mongo.Implementation.QueryProviders.LazyLoading
{
    internal class CacheableDataLoadersProvider : AbstractCacheableServiceProvider<object>, IDataLoadersProvider
    {
        private static IDictionary<string, Type> LoadersTypesDictionary { get; }

        private IServiceProvider ServiceProvider { get; }

        static CacheableDataLoadersProvider()
        {
            LoadersTypesDictionary = typeof(CacheableDataLoadersProvider).Assembly.GetTypes()
                .Where(t => !t.IsAbstract && t.GetInterfaces()
                                .Contains(i =>
                                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDataLoader<>)))
                .ToDictionary(t => t.GetInterfaces()
                    .FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(IDataLoader<>))
                    .GetGenericArguments()[0].ToString());
        }

        public CacheableDataLoadersProvider(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public IDataLoader<TResult> GetDataLoader<TResult>()
        {
            return this.GetServiceInstance(typeof(TResult), typeof(SimpleModelDataLoader<TResult>)) as
                IDataLoader<TResult>;
        }

        protected override IDictionary<string, Type> TypesDictionary => LoadersTypesDictionary;

        protected override object CreateInstance(Type instanceType)
        {
            return ServiceProvider.CreateInstance(instanceType);
        }
    }
}
