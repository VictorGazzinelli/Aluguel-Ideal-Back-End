using AluguelIdeal.Api.Gateways.Interfaces;
using AluguelIdeal.Api.Models.Client;
using AluguelIdeal.Api.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Interactors.Client
{
    public class GetClientsInteractor : IRequestHandler<GetClientsRequest, GetClientsResponse>
    {
        private readonly IClientGateway clientGateway;

        public GetClientsInteractor(IClientGateway clientGateway)
        {
            this.clientGateway = clientGateway;
        }

        public async Task<GetClientsResponse> Handle(GetClientsRequest request, CancellationToken cancellationToken)
        {
            GetClientsResponse response = new GetClientsResponse();

            if (request.Email != null)
                response.Clients = (await clientGateway.GetByEmailAsync(request.Email, cancellationToken)).ConvertToDtoList();
            else if (request.Name != null)
                response.Clients = (await clientGateway.GetByNameAsync(request.Name, cancellationToken)).ConvertToDtoList();

            return response;
        }
    }
}
