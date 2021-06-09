using AluguelIdeal.Application.Interactors.Profiles.Commands;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Application.Transactions;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Profiles.Handlers
{
    public class BindProfileInteractor : IRequestHandler<BindProfileCommand>
    {
        private readonly IProfileRepository profileRepository;
        private readonly ITransactionManager transactionManager;
        public BindProfileInteractor(IProfileRepository profileRepository, ITransactionManager transactionManager)
        {
            this.profileRepository = profileRepository;
            this.transactionManager = transactionManager;
        }

        public async Task<Unit> Handle(BindProfileCommand request, CancellationToken cancellationToken)
        {
            Profile profile = new Profile()
            {
                RoleId = request.RoleId,
                UserId = request.UserId
            };

            await profileRepository.CreateAsync(profile, cancellationToken);

            return Unit.Value;
        }
    }
}
