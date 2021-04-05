using AluguelIdeal.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AluguelIdeal.Api.Controllers.Models.Responses.Http
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ErrorResponse()
        {

        }

        public ErrorResponse(Exception exception)
        {
            this.StatusCode = StatusCodes.Status500InternalServerError;
            this.Message = exception.Message;
        }

        public ErrorResponse(AggregateNotFoundException aggregateNotFoundException)
        {
            this.StatusCode = StatusCodes.Status404NotFound;
            this.Message = aggregateNotFoundException.Message;
        }

        public ObjectResult AsObjectResult()
        {
            return new ObjectResult(this)
            {
                StatusCode = this.StatusCode
            };
        }
    }
}
