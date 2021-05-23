using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Interactors.Residences.Commands;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Residences.Handlers
{
    public class CreateResidenceInteractor : IRequestHandler<CreateResidenceCommand, IdResult>
    {
        private readonly IResidenceRepository residenceRepository;
        public CreateResidenceInteractor(IResidenceRepository residenceRepository)
        {
            this.residenceRepository = residenceRepository;
        }

        public async Task<IdResult> Handle(CreateResidenceCommand request, CancellationToken cancellationToken)
        {
            Guid Id = Guid.NewGuid();

            Residence residence = new Residence()
            {
                Id = Id,
                Area = request.Area,
                Bathrooms = request.Bathrooms,
                Bedrooms = request.Bedrooms,
                Description = request.Description,
                DistrictId = request.DistrictId,
                Rent = request.Rent,
                Street = request.Street,
                Tax = request.Tax,
            };

            await residenceRepository.CreateAsync(residence, cancellationToken);

            return new IdResult() { Id = Id };
        }
    }
}
