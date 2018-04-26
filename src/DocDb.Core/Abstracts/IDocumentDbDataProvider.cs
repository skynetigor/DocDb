using DocDb.Core.Abstracts;

namespace DocDb.Core.Abstracts
{
    public interface IDocumentDbDataProvider
    {
        void RegisterModel<TModel>() where TModel : class;

        void SaveChanges();

        IDbSet<TModel> GetDbSet<TModel>() where TModel : class;
    }
}
