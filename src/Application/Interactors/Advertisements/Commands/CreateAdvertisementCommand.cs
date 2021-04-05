using AluguelIdeal.Application.Dtos.Advertisements;
using MediatR;

namespace AluguelIdeal.Application.Interactors.Advertisements.Commands
{
    public class CreateAdvertisementCommand : IRequest<AdvertisementDto>
    {
        public string Title { get; set; }
    }
}
