using AluguelIdeal.Api.Dto;
using AluguelIdeal.Api.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AluguelIdeal.Api.Utils
{
    public static class ClientExtensions
    {
        public static List<ClientDto> ConvertToDtoList(this IEnumerable<Client> clients) =>
            clients.Select(client => new ClientDto(client))
                   .ToList();
    }
}
