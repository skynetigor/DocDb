using System.Collections.Generic;
using System.Linq;
using DocDb.Core.Abstracts;
using DocDb.Mongo.Abstracts;
using DocDb.Mongo.Implementation;
using DocDb.Mongo.Implementation.QueryProviders.EagerLoading;
using DocDb.Mongo.Implementation.QueryProviders.LazyLoading;
using DocDb.Mongo.Implementation.Serializers.SerializationProvider;
using DocDb.Mongo.Implementation.State;
using DocDb.Mongo.Implementation.TypeMetadataInitializer;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DocDb.Mongo
{
    internal class MongoDbProvider : AbstractDocumentDbDataProvider, IDbsetContainer
    {
        private readonly IList<object> _dbSets = new List<object>();

        private IMongoDatabase Database { get; }
        private ITypeInitializer TypeInititalizer { get; }
        private IStateManager StateManager { get; }
        private MongoDbOptions MongoDbOptions { get; }

        static MongoDbProvider()
        {
            BsonSerializer.RegisterSerializationProvider(new CacheableSerializationProvider(new TypeInitializerImpl()));
        }

        public MongoDbProvider(MongoDbOptions options)
        {
            MongoDbOptions = options;

            var connection = new MongoUrlBuilder(options.ConnectionString);
            var client = new MongoClient(options.ConnectionString);
            this.Database = client.GetDatabase(connection.DatabaseName);

            InitializeProvider();

            TypeInititalizer = ServiceProvider.GetService<ITypeInitializer>();
            StateManager = ServiceProvider.GetService<IStateManager>();
        }

        public override void RegisterModel<T>()
        {
            TypeInititalizer.RegisterType<T>();

            if (MongoDbOptions.DropCollectionsEachTime)
            {
                Database.DropCollection(TypeInititalizer.GetTypeMetadata<T>().CollectionName);
            }

            object dbSet = ServiceProvider.GetService<IDbSet<T>>();
            this._dbSets.Add(dbSet);
        }

        public override void SaveChanges()
        {
            StateManager.SaveChanges();
        }

        public override IDbSet<T> GetDbSet<T>()
        {
            return (IDbSet<T>)_dbSets.FirstOrDefault(db => db is IDbSet<T>);
        }

        protected override void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(MongoDbOptions)
                .AddSingleton<ITypeInitializer, TypeInitializerImpl>()
                .AddSingleton<IMongoDatabase>(this.Database)
                .AddSingleton<IDbsetContainer>(this)
                .AddSingleton(typeof(IDbSet<>), typeof(MongoDbSetSaveChanges<>))
                .AddSingleton<ILazyLoadingInterceptor, LazyLoadingInterceptor>()
                .AddSingleton<ILazyLoadingProxyGenerator, LazyLoadingProxyGenerator>()
                .AddSingleton<IStateManager, StateManager>()
                .AddTransient(typeof(IState<>), typeof(State<>))
                .AddTransient(typeof(LazyLoadingQueryProvider<>))
                .AddTransient(typeof(EagerLoadingQueryProvider<>))
                .AddTransient(typeof(ILazyLoadingIncludableQueryable<>), typeof(LazyLoadingIncludableQueryable<>))
                .AddTransient(typeof(IEagerLoadingIncludableQueryable<>), typeof(EagerLoadingIncludableQueryable<>))
                .AddTransient<IDataLoadersProvider, CacheableDataLoadersProvider>();
        }
    }
}
