using System;
using DocDb.Core.Abstracts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.InMemory.Query.ExpressionVisitors.Internal;
using Microsoft.EntityFrameworkCore.InMemory.Query.Internal;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.DependencyInjection;
using Remotion.Linq.Parsing.ExpressionVisitors.TreeEvaluation;

namespace DocDb.EntityFrameworkAdapter
{
    public class DocumentDbContextOptionsExtension : IDbContextOptionsExtension, IDocumentDbOptions
    {
        public DocumentDbContextOptionsExtension(Action<DocumentDbOptionsBuilder> builderAction)
        {
            var builder = new DocumentDbOptionsBuilder();

            builderAction(builder);

            ProviderInstance = builder.DocumentDbOptions.ProviderInstance;
        }

        public string LogFragment => throw new NotImplementedException();

        public bool ApplyServices(IServiceCollection services)
        {
            var builder = new EntityFrameworkServicesBuilder(services)
                .TryAdd<IDatabaseProvider, DatabaseProvider<DocumentDbContextOptionsExtension>>()
                .TryAdd<IValueGeneratorSelector, InMemoryValueGeneratorSelector>()
                .TryAdd<IDatabase>(p => p.GetService<IInMemoryDatabase>())
                .TryAdd<IDbContextTransactionManager, InMemoryTransactionManager>()
                .TryAdd<IDatabaseCreator, InMemoryDatabaseCreator>()
                .TryAdd<IQueryContextFactory, InMemoryQueryContextFactory>()
                .TryAdd<IEntityQueryModelVisitorFactory, InMemoryQueryModelVisitorFactory>()
                .TryAdd<IEntityQueryableExpressionVisitorFactory, InMemoryEntityQueryableExpressionVisitorFactory>()
                .TryAdd<IEvaluatableExpressionFilter, EvaluatableExpressionFilter>()
                .TryAdd(typeof(IDbSetSource), typeof(DocumentDbSetSource))
                .TryAdd<ISingletonOptions, DocumentDbSingletonOptions>(p => p.GetService<DocumentDbSingletonOptions>());
                //.TryAdd<ISingletonOptionsInitialzer, SingletonOptionsInitialzer>()

            return true;
        }

        public long GetServiceProviderHashCode()
        {
           return this.GetHashCode();
        }

        public void Validate(IDbContextOptions options)
        {
            
        }

        public IDocumentDbDataProvider ProviderInstance { get; }
    }
}
