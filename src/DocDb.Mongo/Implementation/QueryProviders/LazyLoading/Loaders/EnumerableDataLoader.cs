﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DocDb.Mongo.Abstracts;
using DocDb.Mongo.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DocDb.Mongo.Implementation.QueryProviders.LazyLoading.Loaders
{
    class EnumerableDataLoader<TResult> : IDataLoader<IEnumerable<TResult>>
    {
        private IMongoDatabase Database { get; }
        private ITypeInitializer TypeInitializer { get; }
        private ILazyLoadingProxyGenerator ProxyGenerator { get; }

        public EnumerableDataLoader(IMongoDatabase database, ITypeInitializer typeInitializer, ILazyLoadingProxyGenerator interceptor)
        {
            Database = database;
            TypeInitializer = typeInitializer;
            this.ProxyGenerator = interceptor;
        }

        public IEnumerable<TResult> LoadData<TSource>(TSource source, PropertyInfo loadedProperty)
        {
            var resultTypeMetadata = this.TypeInitializer.GetTypeMetadata<TResult>();

            var navPropName = typeof(TResult).Name;

            var navigationProperty = typeof(TResult).GetProperties().FirstOrDefault(p => p.PropertyType == typeof(TSource));

            var queryList = new List<BsonDocument>
            {
                new BsonDocument("$match",
                    new BsonDocument(navigationProperty.GetNavigationPropertyName(), resultTypeMetadata.IdProperty.GetValue(source).ToString())),
            };

            PipelineDefinition<TResult, TResult> pipeline = queryList;

            var targets = this.Database.GetCollection<TResult>(resultTypeMetadata.CollectionName).Aggregate(pipeline).ToEnumerable();

            return this.ProxyGenerator.CreateProxies(targets);
        }
    }
}
