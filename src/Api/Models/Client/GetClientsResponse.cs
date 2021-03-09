using AluguelIdeal.Api.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AluguelIdeal.Api.Models.Client
{
    public class GetClientsResponse
    {
        public List<ClientDto> Clients { get; set; } = new List<ClientDto>();

        public override bool Equals(object obj) =>
            obj is GetClientsResponse response &&
            Enumerable.SequenceEqual(Clients, response.Clients);

        public override int GetHashCode() =>
            HashCode.Combine(Clients);
    }
}
