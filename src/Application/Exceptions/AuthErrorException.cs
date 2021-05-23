using System;
using System.Runtime.Serialization;

namespace AluguelIdeal.Application.Exceptions
{
    [Serializable]
    public class AuthErrorException : Exception
    {
        public AuthErrorException()
        {
        }

        public AuthErrorException(string message) : base(message)
        {
        }

        public AuthErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AuthErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
