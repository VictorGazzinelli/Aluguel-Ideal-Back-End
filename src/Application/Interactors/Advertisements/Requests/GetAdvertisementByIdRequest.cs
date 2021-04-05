using AluguelIdeal.Application.Interactors.Advertisements.Responses;
using MediatR;
using System;

namespace AluguelIdeal.Application.Interactors.Advertisements.Requests
{
    public class GetAdvertisementByIdRequest : IRequest<GetAdvertisementByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
