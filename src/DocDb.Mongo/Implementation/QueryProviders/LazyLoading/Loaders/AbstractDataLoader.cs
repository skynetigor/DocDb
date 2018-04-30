﻿using System.Collections.Generic;
using System.Reflection;
using DocDb.Core.DI.Abstract;
using DocDb.Core.DI.Extensions;
using DocDb.Mongo.Abstracts;

namespace DocDb.Mongo.Implementation.QueryProviders.LazyLoading.Loaders
{
    abstract class AbstractDataLoader<TModel, TResult> : IDataLoader<TResult>
    {
        protected IDocDbServiceProvider ServiceProvider { get; }

        protected IDataLoader<IEnumerable<TModel>> EnumerableDataLoader =>
            this.ServiceProvider.CreateInstance<EnumerableDataLoader<TModel>>();

        protected AbstractDataLoader(IDocDbServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public abstract TResult LoadData<TSource>(TSource source, PropertyInfo loadedProperty);
    }
}
