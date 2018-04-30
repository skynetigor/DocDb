using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DocDb.Core.Extensions;

namespace DocDb.Core.DI.Extensions
{
    internal static class ServiceProviderExtensions
    {
        public static object CreateInstance(this IServiceProvider serviceProvider, ConstructorInfo constructorInfo,
            Type instanceType,
            params object[] parameters)
        {
            var ctorParameterInfos = constructorInfo.GetParameters();
            var ctorParametersValues = new object[ctorParameterInfos.Length];

            for (int i = 0; i < ctorParameterInfos.Length; i++)
            {
                var ctorParameter = ctorParameterInfos[i];
                object instance = null;

                object fromParam = parameters.FirstOrDefault(p => ctorParameter.ParameterType.IsInstanceOfType(p));

                instance = fromParam.IsNotNull() ? fromParam : serviceProvider.GetService(ctorParameter.ParameterType);

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
