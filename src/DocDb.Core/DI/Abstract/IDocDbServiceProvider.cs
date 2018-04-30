using System;

namespace DocDb.Core.DI.Abstract
{
    public interface IDocDbServiceProvider : IServiceProvider
    {
        object CreateInstanceWithParameters(Type instanceType, params object[] parameters);

        object CreateInstance(Type instanceType);
    }
}
