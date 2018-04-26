namespace DocDb.Core.Abstracts
{
    public interface IDocumentDbOptions
    {
        IDocumentDbDataProvider ProviderInstance { get; }
    }
}
