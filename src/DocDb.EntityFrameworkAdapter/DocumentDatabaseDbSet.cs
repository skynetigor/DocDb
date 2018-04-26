using System.Threading;
using System.Threading.Tasks;
using DocDb.Core.Abstracts;
using DocDb.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DocDb.EntityFrameworkAdapter
{
    internal class DocumentDatabaseDbSet<TEntity>: DbSet<TEntity> where TEntity: class
    {
        private IDbSet<TEntity> _docDbSet;

        private IDocumentDbDataProvider Provider { get; }

        private IDbSet<TEntity> DocDbSet
        {
            get
            {
                if (_docDbSet == null)
                {
                    _docDbSet = Provider.GetDbSet<TEntity>();
                }

                return _docDbSet;
            }
        }

        public DocumentDatabaseDbSet(IDocumentDbDataProvider provider)
        {
            Provider = provider;
            provider.RegisterModel<TEntity>();
        }


        public override EntityEntry<TEntity> Add(TEntity entity)
        {
            DocDbSet.Add(entity);
            return null;
        }

        public override async Task<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            await DocDbSet.AddAsync(entity, cancellationToken);
            return null;
        }
    }
}
