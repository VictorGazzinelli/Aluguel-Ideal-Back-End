using System;
using System.Runtime.Serialization;

namespace AluguelIdeal.Api.Database
{
    [Serializable]
    public class CouldNotFindDatabaseException : Exception
    {
        public CouldNotFindDatabaseException(string message = null, Exception innerException = null) : base(message, innerException)
        {
           
        }

        protected CouldNotFindDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
