using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DocDb.Core.DI.Extensions
{
    public static class ServiceProviderExtensions
    {
        private const string UnKnownParameterError = "Unable to find suitable constructor for type \"{0}\"";

        public static TService CreateInstance<TService>(this IServiceProvider serviceProvider) where TService: class
        {
            return (TService)CreateInstance(serviceProvider, typeof(TService));
        }

        public static object CreateInstance(this IServiceProvider serviceProvider, Type instanceType)
        {
            var constructorInfo = instanceType.GetConstructors();

            for (int i = constructorInfo.Length - 1; i >= 0; i--)
            {
                var instance = CreateInstance(serviceProvider, constructorInfo[i], instanceType);

                if (instance != null)
                {
                    return instance;
                }
            }

            throw new InvalidOperationException(string.Format(UnKnownParameterError, instanceType.Name));
        }

        private static object CreateInstance(IServiceProvider serviceProvider, ConstructorInfo constructorInfo, Type instanceType)
        {
            var valuesList = new List<object>();
            var ctorParameterInfos = constructorInfo.GetParameters();
            var ctorParametersValues = new object[ctorParameterInfos.Length];

            for (int i = 0; i < ctorParameterInfos.Length; i++)
            {
                var ctorParameter = ctorParameterInfos[i];
                var instance = serviceProvider.GetService(ctorParameter.ParameterType);

                if (instance == null)
                {
                    return null;
                }

                ctorParametersValues[i] = instance;
            }

            return Activator.CreateInstance(instanceType, ctorParametersValues);
        }
    }
}
