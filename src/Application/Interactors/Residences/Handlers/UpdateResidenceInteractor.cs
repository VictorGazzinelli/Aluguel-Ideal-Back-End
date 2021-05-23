using AluguelIdeal.Application.Interactors.Residences.Commands;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Residences.Handlers
{
    public class UpdateResidenceInteractor : IRequestHandler<UpdateResidenceCommand>
    {
        private readonly IResidenceRepository residenceRepository;
        public UpdateResidenceInteractor(IResidenceRepository residenceRepository)
        {
            this.residenceRepository = residenceRepository;
        }

        public async Task<Unit> Handle(UpdateResidenceCommand request, CancellationToken cancellationToken)
        {
            Residence residence = new Residence()
            {
                Id = request.Id,
                Area = request.Area,
                Bathrooms = request.Bathrooms,
                Bedrooms = request.Bedrooms,
                Description = request.Description,
                DistrictId = request.DistrictId,
                Rent = request.Rent,
                Street = request.Street,
                Tax = request.Tax,
            };

            await residenceRepository.UpdateAsync(residence, cancellationToken);

            return Unit.Value;
        }
    }
}
