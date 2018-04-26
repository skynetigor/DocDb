using DocDb.Core.Abstracts;

namespace DocDb.Mongo.Abstracts
{
    internal interface IEagerLoadingIncludableQueryable<T>: IIncludableQueryable<T>
    {
    }
}
