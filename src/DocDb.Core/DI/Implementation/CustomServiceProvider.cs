using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DocDb.Core.DI.Abstract;
using DocDb.Core.DI.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DocDb.Core.DI.Implementation
{
    internal class DocDbServiceProvider : IDocDbServiceProvider
    {
        private const string UnKnownParameterError = "Unable to find suitable constructor for type \"{0}\"";

        private readonly IDictionary<Type, ConstructorInfo> _constructors = new Dictionary<Type, ConstructorInfo>();

        private IServiceProvider ServiceProvider { get; }

        public DocDbServiceProvider(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IServiceProvider>(this);
            serviceCollection.AddSingleton<IDocDbServiceProvider>(this);
            this.ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public object GetService(Type serviceType)
        {
            return this.ServiceProvider.GetService(serviceType);
        }

        public object CreateInstanceWithParameters(Type instanceType,
            params object[] parameters)
        {
            var m = instanceType.GetConstructors();
            IEnumerable<ConstructorInfo> constructorInfos = instanceType.GetConstructors().Where(c =>
                c.GetParameters().FirstOrDefault(p =>
                    parameters.FirstOrDefault(k => k.GetType() == p.ParameterType || p.ParameterType.IsInstanceOfType(k)) != null) != null);

            foreach (var ctor in constructorInfos)
            {
                var instance = ServiceProvider.CreateInstance(ctor, instanceType, parameters);

                if (instance != null)
                {
                    return instance;
                }
            }

            return null;
        }

        public object CreateInstance(Type instanceType)
        {
            if (_constructors.TryGetValue(instanceType, out var ctor))
            {
                return ServiceProvider.CreateInstance(ctor, instanceType);
            }

            var constructorInfos = instanceType.GetConstructors();

            for (int i = constructorInfos.Length - 1; i >= 0; i--)
            {
                ConstructorInfo ctorInfo = constructorInfos[i];
                var instance = ServiceProvider.CreateInstance(ctorInfo, instanceType);

                if (instance != null)
                {
                    _constructors.Add(instanceType, ctorInfo);
                    return instance;
                }
            }

            return null;
        }
    }
}
