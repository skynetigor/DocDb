using System;
using System.Collections.Generic;
using System.Reflection;
using DocDb.Core.DI.Abstract;
using DocDb.Mongo.Models;

namespace DocDb.Mongo.Implementation.QueryProviders.LazyLoading.Loaders
{
    class CollectionsLoader<T> : AbstractDataLoader<T, ICollection<T>>
    {
        public CollectionsLoader(IDocDbServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ICollection<T> LoadData<TSource>(TSource source, PropertyInfo loadedProperty)
        {
            return TrackingList<T>.CreateExistingTrackingList(this.EnumerableDataLoader.LoadData(source, loadedProperty));
        }
    }
}
