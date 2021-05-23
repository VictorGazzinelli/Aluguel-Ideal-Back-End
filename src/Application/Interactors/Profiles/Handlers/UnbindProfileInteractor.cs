using AluguelIdeal.Application.Interactors.Profiles.Commands;
using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Profiles.Handlers
{
    public class UnbindProfileInteractor : IRequestHandler<UnbindProfileCommand>
    {
        private readonly IProfileRepository profileRepository;
        public UnbindProfileInteractor(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        public async Task<Unit> Handle(UnbindProfileCommand request, CancellationToken cancellationToken)
        {
            Profile profile = new Profile()
            {
                RoleId = request.RoleId,
                UserId = request.UserId
            };

            await profileRepository.Delete(profile, cancellationToken);

            return Unit.Value;
        }
    }
}
