namespace DocDb.Mongo.Abstracts
{
    interface IStateManager: IChangesSaver
    {
        IState<T> GetOrCreateState<T>() where T : class;
    }
}
