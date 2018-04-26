using System;
using DocDb.Core.DI.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace DocDb.Core.DI.Implementation
{
    public class CustomServiceCollection: ServiceCollection, ICustomServiceCollection
    {
        public IServiceProvider BuildServiceProvider()
        {
            return new CustomServiceProvider(this);
        }
    }
}
