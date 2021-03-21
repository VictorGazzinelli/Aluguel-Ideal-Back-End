using AluguelIdeal.Api.Utils.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Runtime.Serialization;

namespace AluguelIdeal.Api.Interactors.Behaviours
{
    [Serializable]
    public class BadRequestException : HttpResponseException
    {
        public override int Status { get; set; } = StatusCodes.Status400BadRequest;
        public override bool IsMessageUserFriendly { get; set; } = false;
        public override IDictionary Data { get; }

        public BadRequestException(IDictionary data = null, string message = null, bool isMessageUserFriendly = false, Exception innerException = null) 
            : base(message, innerException)
        {
            this.Data = data;
            this.IsMessageUserFriendly = isMessageUserFriendly;
        }

        protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            
        }
    }
}