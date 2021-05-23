using AluguelIdeal.Application.Interactors.Profiles.Commands;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Profiles.Handlers
{
    public class BindProfileInteractor : IRequestHandler<BindProfileCommand>
    {
        private readonly IProfileRepository profileRepository;
        public BindProfileInteractor(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        public async Task<Unit> Handle(BindProfileCommand request, CancellationToken cancellationToken)
        {
            Profile profile = new Profile()
            {
                RoleId = request.RoleId,
                UserId = request.UserId
            };

            await profileRepository.Create(profile, cancellationToken);

            return Unit.Value;
        }
    }
}
