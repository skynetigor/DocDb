using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DocDb.Core.Abstracts;
using DocDb.Core.DI.Extensions;
using DocDb.Core.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace DocDb.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string ProviderIsNotConfiguredForThisContext = "Provider isn't configured for this context";

        public static IServiceCollection UseDocumentDbContext<TContext>(this IServiceCollection serviceCollection,
            IDocumentDbOptions options, ServiceLifetime serviceLifetime) where TContext : DocumentDbContext
        {
            return UseDocumentDbContext<TContext>(serviceCollection, bld => bld.UseOptions(options),
                serviceLifetime);
        }

        public static IServiceCollection UseDocumentDbContext<TContext>(this IServiceCollection serviceCollection,
            Action<DocumentDbOptionsBuilder> builderFunc, ServiceLifetime serviceLifetime)
            where TContext : DocumentDbContext
        {
            serviceCollection.Add(new ServiceDescriptor(typeof(TContext), sp => ContextInstanceCreator<TContext>.CreateContextInstance(sp, builderFunc),
                serviceLifetime));
            return serviceCollection;
        }
    }
}
