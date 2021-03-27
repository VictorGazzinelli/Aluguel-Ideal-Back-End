using Microsoft.AspNetCore.Http;
using System;
using System.Runtime.Serialization;

namespace AluguelIdeal.Api.Utils.Exceptions
{
    public abstract class HttpResponseException : Exception
    {
        public virtual int Status { get; set; } = StatusCodes.Status500InternalServerError;
        public virtual bool IsMessageUserFriendly { get; set; } = false;

        protected HttpResponseException()
        {
        }

        protected HttpResponseException(string message) : base(message)
        {
        }

        protected HttpResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
