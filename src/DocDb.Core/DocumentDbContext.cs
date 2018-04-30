using System;
using System.Linq;
using DocDb.Core.Abstracts;
using DocDb.Core.Extensions;

namespace DocDb.Core
{
    public abstract class DocumentDbContext
    {
        private IDocumentDbDataProvider Provider { get; }

        protected DocumentDbContext(IDocumentDbOptions options)
        {
            this.Provider = options.ProviderInstance;
            Setup();
        }

        protected DocumentDbContext(Action<DocumentDbOptionsBuilder> builderFunc) : this(
            DocumentDbOptionsBuilder.GetOptionsFromBuilderAction(builderFunc)) { }

        public void SaveChanges()
        {
            this.Provider.SaveChanges();
        }

        public IDbSet<T> Set<T>()
            where T : class
        {
            return this.Provider.GetDbSet<T>();
        }

        private void Setup()
        {
            var items = this.GetProperties().Where(p => p.PropertyType.GetGenericTypeDefinition() == typeof(IDbSet<>))
                .ToArray();

            foreach (var prop in items)
            {
                var modelType = prop.PropertyType.GetGenericArguments()[0];
                this.Provider.RegisterModel(modelType);
            }

            foreach (var prop in items)
            {
                var modelType = prop.PropertyType.GetGenericArguments()[0];
                var instance = this.Provider.GetDbSet(modelType);
                prop.SetValue(this, instance);
            }
        }
    }
}