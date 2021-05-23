using System;
using System.Runtime.Serialization;

namespace AluguelIdeal.Infrastructure.Exceptions
{
    [Serializable]
    public class UnexpectedDatabaseBehaviourException : Exception
    {
        public UnexpectedDatabaseBehaviourException()
        {
        }

        public UnexpectedDatabaseBehaviourException(string message) : base(message)
        {
        }

        public UnexpectedDatabaseBehaviourException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnexpectedDatabaseBehaviourException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
