using System;
using Microsoft.Extensions.DependencyInjection;

namespace DocDb.Core.DI.Implementation
{
    internal class CustomServiceProvider : IServiceProvider
    {
        private const string UnKnownParameterError = "Unable to find suitable constructor for type \"{0}\"";

        private IServiceProvider ServiceProvider { get; }

        public CustomServiceProvider(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IServiceProvider>(this);
            this.ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public object GetService(Type serviceType)
        {
            return this.ServiceProvider.GetService(serviceType);
        }
    }
}
