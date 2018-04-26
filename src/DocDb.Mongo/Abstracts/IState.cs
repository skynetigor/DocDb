using DocDb.Core.Abstracts;

namespace DocDb.Mongo.Abstracts
{
    interface IState<in TEntity>: IDataProcessor<TEntity>, IChangesSaver where TEntity: class 
    {

    }
}
