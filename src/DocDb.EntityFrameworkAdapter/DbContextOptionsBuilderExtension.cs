using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DocDb.EntityFrameworkAdapter
{
    public static class DbContextOptionsBuilderExtension
    {
        public static void UseDocumentDb(this DbContextOptionsBuilder builder, Action<DocumentDbOptionsBuilder> builderAction)
        {
            ((IDbContextOptionsBuilderInfrastructure)builder).AddOrUpdateExtension(new DocumentDbContextOptionsExtension(builderAction));
        }
    }
}
