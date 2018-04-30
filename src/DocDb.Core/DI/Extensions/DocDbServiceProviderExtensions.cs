using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DocDb.Core.DI.Abstract;

namespace DocDb.Core.DI.Extensions
{
    public static class DocDbServiceProviderExtensions
    {
        private const string UnKnownParameterError = "Unable to find suitable constructor for type \"{0}\"";

        public static TService CreateInstance<TService>(this IDocDbServiceProvider serviceProvider) where TService : class
        {
            return (TService) serviceProvider.CreateInstance(typeof(TService));
        }

        public static TService CreateInstanceWithParameters<TService>(this IDocDbServiceProvider serviceProvider, params object[] parameters) where TService : class
        {
            return (TService)serviceProvider.CreateInstanceWithParameters(typeof(TService), parameters);
        }
    }
}
