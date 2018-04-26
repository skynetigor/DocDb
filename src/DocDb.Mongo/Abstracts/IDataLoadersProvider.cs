namespace DocDb.Mongo.Abstracts
{
    internal interface IDataLoadersProvider
    {
        IDataLoader<TResult> GetDataLoader<TResult>();
    }
}
