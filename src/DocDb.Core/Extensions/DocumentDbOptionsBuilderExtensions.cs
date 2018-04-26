using DocDb.Core.Abstracts;

namespace DocDb.Core.Extensions
{
    public static class DocumentDbOptionsBuilderExtensions
    {
        public static void UseProvider(this DocumentDbOptionsBuilder builder, IDocumentDbOptions options)
        {
            builder.ChangeDatabaseOptions(options);
        }
    }
}
