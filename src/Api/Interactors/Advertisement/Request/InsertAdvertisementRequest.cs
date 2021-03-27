﻿using AluguelIdeal.Api.Interactors.Advertisement.Response;
using MediatR;

namespace AluguelIdeal.Api.Interactors.Advertisement.Request
{
    public class InsertAdvertisementRequest : IRequest<InsertAdvertisementResponse>
    {
        public string Title { get; set; }
    }
}
