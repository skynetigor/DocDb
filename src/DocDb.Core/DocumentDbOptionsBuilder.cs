using System;
using DocDb.Core.Abstracts;

namespace DocDb.Core
{
    public class DocumentDbOptionsBuilder
    {
        private IDocumentDbOptions _documentDbOptions;

        internal static IDocumentDbOptions GetOptionsFromBuilderAction(Action<DocumentDbOptionsBuilder> action)
        {
            var builder = new DocumentDbOptionsBuilder();
            action(builder);

            return builder._documentDbOptions;
        }

        private DocumentDbOptionsBuilder() { }

        public void UseOptions(IDocumentDbOptions options)
        {
            _documentDbOptions = options;
        }
    }
}
