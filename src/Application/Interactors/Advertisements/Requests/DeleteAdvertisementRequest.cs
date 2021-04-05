using MediatR;
using System;

namespace AluguelIdeal.Application.Interactors.Advertisements.Requests
{
    public class DeleteAdvertisementRequest : IRequest
    {
        public Guid Id { get; set; }
    }
}
