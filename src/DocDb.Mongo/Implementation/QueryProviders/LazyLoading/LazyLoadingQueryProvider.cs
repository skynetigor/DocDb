using System.Collections.Generic;
using System.Linq.Expressions;
using DocDb.Mongo.Abstracts;
using MongoDB.Driver;

namespace DocDb.Mongo.Implementation.QueryProviders.LazyLoading
{
    internal class LazyLoadingQueryProvider<T> : AbstractQueryProviderFromPipeline<T> where T : class
    {
        private ILazyLoadingProxyGenerator ProxyGenerator { get; }

        public LazyLoadingQueryProvider(IMongoDatabase database, ITypeInitializer typeInitializer, ILazyLoadingProxyGenerator interceptor) : base(database, typeInitializer)
        {
            this.ProxyGenerator = interceptor;
        }

        public override object Execute(Expression expression)
        {
            var data = base.Execute(expression);

            switch (data)
            {
                case IEnumerable<T> enumerable:
                    return this.ProxyGenerator.CreateProxies(enumerable);
                case T entity:
                    return this.ProxyGenerator.CreateProxy(entity);
            }

            return data;
        }
    }
}
