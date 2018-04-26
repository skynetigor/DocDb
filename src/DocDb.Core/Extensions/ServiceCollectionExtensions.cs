using System;
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
            serviceCollection.AddSingleton<IDocumentDbOptions>(options);
            serviceCollection.Add(new ServiceDescriptor(typeof(TContext), sp => sp.CreateInstance<TContext>(),
                serviceLifetime));

            return serviceCollection;
        }

        public static IServiceCollection UseDocumentDbContext<TContext>(this IServiceCollection serviceCollection,
            Action<DocumentDbOptionsBuilder> builderFunc, ServiceLifetime serviceLifetime)
            where TContext : DocumentDbContext
        {
            var builder = new DocumentDbOptionsBuilder();
            builderFunc(builder);

            if (builder.DocumentDbOptions.IsNotNull())
            {
                UseDocumentDbContext<TContext>(serviceCollection, builder.DocumentDbOptions,
                    serviceLifetime);

                return serviceCollection;
            }

            throw new ContextConfigutationException(ProviderIsNotConfiguredForThisContext);
        }
    }
}
