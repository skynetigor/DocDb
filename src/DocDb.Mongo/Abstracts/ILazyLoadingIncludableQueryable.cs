using DocDb.Core.Abstracts;

namespace DocDb.Mongo.Abstracts
{
    internal interface ILazyLoadingIncludableQueryable<T>: IIncludableQueryable<T>
    {
    }
}
