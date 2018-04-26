using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DocDb.EntityFrameworkAdapter
{
    class DocumentDbSetSource: IDbSetSource
    {
        public IServiceProvider ServiceProvider { get; }

        public DocumentDbSetSource(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public object Create(DbContext context, Type type)
        {
            return ServiceProvider.GetService(typeof(DocumentDatabaseDbSet<>).MakeGenericType(type));
        }
    }
}
