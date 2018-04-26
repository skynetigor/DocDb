using System;
using System.Collections.Generic;
using System.Text;
using DocDb.Core.Abstracts;

namespace DocDb
{
    public class DocumentDbOptionsBuilder
    {
        private IDocumentDbOptions documentDbOptions;
        public IDocumentDbOptions DocumentDbOptions => documentDbOptions;

        public void ChangeDatabaseOptions(IDocumentDbOptions options)
        {
            documentDbOptions = options;
        }
    }
}
