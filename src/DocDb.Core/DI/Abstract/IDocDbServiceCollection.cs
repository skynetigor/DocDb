using System;
using Microsoft.Extensions.DependencyInjection;

namespace DocDb.Core.DI.Abstract
{
    internal interface IDocDbServiceCollection: IServiceCollection
    {
        IDocDbServiceProvider BuildDocDbServiceProvider();
    }
}
