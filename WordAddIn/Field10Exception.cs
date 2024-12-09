using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordAddIn
{
    [Serializable]
    internal class LQ10FieldException : Exception
    {
        public LQ10FieldException()
        {
        }

        public LQ10FieldException(string message) : base(message)
        {
        }

        public LQ10FieldException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
