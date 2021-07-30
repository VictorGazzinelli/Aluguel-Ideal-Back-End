using AluguelIdeal.Application.Interactors.Common;
using AluguelIdeal.Application.Interactors.Residences.Commands;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Residences.Handlers
{
    public class CreateResidenceInteractor : IRequestHandler<CreateResidenceCommand, IdResult>
    {
        private readonly IResidenceRepository residenceRepository;
        private readonly IMapper mapper;
        public CreateResidenceInteractor(IResidenceRepository residenceRepository, IMapper mapper)
        {
            this.residenceRepository = residenceRepository;
            this.mapper = mapper;
        }

        public async Task<IdResult> Handle(CreateResidenceCommand request, CancellationToken cancellationToken)
        {
            Guid Id = Guid.NewGuid();

            Residence residence = mapper.Map<CreateResidenceCommand, Residence>(request);

            if(residence != null)
                residence.Id = Id;

            await residenceRepository.CreateAsync(residence, cancellationToken);

            return new IdResult() { Id = Id };
        }
    }
}
