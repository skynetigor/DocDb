using System;
using System.Collections.Generic;
using System.Text;
using DocDb.Core.DI.Abstract;
using DocDb.Core.DI.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace DocDb.Core.Abstracts
{
    public abstract class AbstractDocumentDbDataProvider : IDocumentDbDataProvider
    {
        private IDocDbServiceProvider _serviceProvider;

        protected IDocDbServiceProvider ServiceProvider => _serviceProvider;

        public abstract IDbSet<TModel> GetDbSet<TModel>() where TModel : class;

        public abstract void RegisterModel<TModel>() where TModel : class;

        public abstract void SaveChanges();

        protected void InitializeProvider()
        {
            IDocDbServiceCollection serviceCollection = new DocDbServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildDocDbServiceProvider();
        }

        protected abstract void ConfigureServices(IServiceCollection serviceCollection);
    }
}
