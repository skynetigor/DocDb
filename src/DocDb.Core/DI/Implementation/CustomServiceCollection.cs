using System;
using DocDb.Core.DI.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace DocDb.Core.DI.Implementation
{
    internal class DocDbServiceCollection: ServiceCollection, IDocDbServiceCollection
    {
        public IDocDbServiceProvider BuildDocDbServiceProvider()
        {
            return new DocDbServiceProvider(this);
        }
    }
}
