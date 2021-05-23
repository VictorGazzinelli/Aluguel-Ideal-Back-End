using System;
using System.Runtime.Serialization;

namespace AluguelIdeal.Application.Exceptions
{
    [Serializable]
    public class UserHasAlreadyBeenRegisteredException : Exception
    {
        public UserHasAlreadyBeenRegisteredException()
        {
        }

        public UserHasAlreadyBeenRegisteredException(string message) : base(message)
        {
        }

        public UserHasAlreadyBeenRegisteredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserHasAlreadyBeenRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
