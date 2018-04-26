using System;

namespace DocDb.Core.Exceptions
{
    class ContextConfigutationException : Exception
    {
        public ContextConfigutationException(string message) : base(message)
        {
        }

        public ContextConfigutationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
