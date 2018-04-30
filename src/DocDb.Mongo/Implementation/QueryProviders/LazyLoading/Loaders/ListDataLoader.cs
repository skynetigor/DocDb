using System.Collections.Generic;
using System.Reflection;
using DocDb.Core.DI.Abstract;
using DocDb.Mongo.Models;

namespace DocDb.Mongo.Implementation.QueryProviders.LazyLoading.Loaders
{
    class ListDataLoader<T>: AbstractDataLoader<T, IList<T>>
    {
        public ListDataLoader(IDocDbServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override IList<T> LoadData<TSource>(TSource source, PropertyInfo loadedProperty)
        {
            return TrackingList<T>.CreateExistingTrackingList(this.EnumerableDataLoader.LoadData(source, loadedProperty));
        }
    }
}
