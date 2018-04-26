using DocDb.Core.Abstracts;

namespace DocDb.Mongo.Abstracts
{
    internal interface IDbsetContainer
    {
        IDbSet<T> GetDbSet<T>() where T: class;
    }
}
