using AluguelIdeal.Application.Interactors.Profiles.Commands;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace AluguelIdeal.Application.Interactors.Profiles.Handlers
{
    public class BindProfileInteractor : IRequestHandler<BindProfileCommand>
    {
        private readonly IProfileRepository profileRepository;
        private readonly IControladorDeTransacao controladorDeTransacao;
        public BindProfileInteractor(IProfileRepository profileRepository, IControladorDeTransacao controladorDeTransacao)
        {
            this.profileRepository = profileRepository;
            this.controladorDeTransacao = controladorDeTransacao;
        }

        public async Task<Unit> Handle(BindProfileCommand request, CancellationToken cancellationToken)
        {
            Profile profile = new Profile()
            {
                RoleId = request.RoleId,
                UserId = request.UserId
            };

            await controladorDeTransacao.FuncaoSemRetorno(async () =>
            {
                await profileRepository.CreateAsync(profile, cancellationToken);
            });

            return Unit.Value;
        }
    }
}
