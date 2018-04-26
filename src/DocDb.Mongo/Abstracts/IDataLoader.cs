using System.Reflection;

namespace DocDb.Mongo.Abstracts
{
    internal interface IDataLoader<TResult>
    {
        TResult LoadData<TSource>(TSource source, PropertyInfo loadedProperty);
    }
}
