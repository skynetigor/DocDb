using System;
using Microsoft.Extensions.DependencyInjection;


namespace DocDb.Core.DI.Abstract
{
    public interface ICustomServiceCollection: IServiceCollection
    {
        IServiceProvider BuildServiceProvider();
    }
}
