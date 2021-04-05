using AluguelIdeal.Application.Dtos.Advertisements;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AluguelIdeal.Application.Interactors.Advertisements.Queries
{
    public class GetAdvertisementsByIdQuery : IRequest<AdvertisementDto>
    {
        [FromRoute]
        public Guid Id { get; set; }
    }
}
