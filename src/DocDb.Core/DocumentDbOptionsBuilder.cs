using DocDb.Core.Abstracts;

namespace DocDb.Core
{
    public class DocumentDbOptionsBuilder
    {
        private IDocumentDbOptions documentDbOptions;

        internal IDocumentDbOptions DocumentDbOptions => documentDbOptions;

        public void UseOptions(IDocumentDbOptions options)
        {
            documentDbOptions = options;
        }
    }
}
