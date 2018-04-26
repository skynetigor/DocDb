using System;
using System.Linq;
using System.Linq.Expressions;

namespace DocDb.Core.Abstracts
{
    public interface IIncludableQueryable<TEntity>: IQueryable<TEntity>
    {
        IQueryable<TEntity> Include();

        IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] navigationPropsPath);

        IQueryable<TEntity> Include(params string[] navigationPropsPath);
    }
}
