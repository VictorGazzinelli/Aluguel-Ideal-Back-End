﻿using AluguelIdeal.Application.Interactors.Advertisements.Responses;
using MediatR;

namespace AluguelIdeal.Application.Interactors.Advertisements.Requests
{
    public class InsertAdvertisementRequest : IRequest<InsertAdvertisementResponse>
    {
        public string Title { get; set; }
    }
}
