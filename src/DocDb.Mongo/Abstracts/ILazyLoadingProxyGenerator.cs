using System.Collections.Generic;

namespace DocDb.Mongo.Abstracts
{
    internal interface ILazyLoadingProxyGenerator
    {
        T CreateProxy<T>(T target);

        IEnumerable<T> CreateProxies<T>(IEnumerable<T> targets);
    }
}
