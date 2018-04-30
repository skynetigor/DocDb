namespace DocDb.Core.Abstracts
{
    public interface IDbSet<TEntity> : IDataProcessor<TEntity>
        where TEntity : class
    {
        IIncludableQueryable<TEntity> UseLazyLoading();

        IIncludableQueryable<TEntity> UseEagerLoading();
    }
}
