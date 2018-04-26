using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace DocDb.EntityFrameworkAdapter
{
    class DocumentDbEfProvider: IDatabaseProvider
    {
        public bool IsConfigured(IDbContextOptions options)
        {
            throw new NotImplementedException();
        }

        public string Name { get; }
    }
}
