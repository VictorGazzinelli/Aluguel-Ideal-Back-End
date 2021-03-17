using AluguelIdeal.Api.Repositories.Interfaces;
using AluguelIdeal.Api.Interactors.Contact.Request;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Api.Interactors.Contact
{
    public sealed class DeleteContactInteractor : IRequestHandler<DeleteContactRequest>
    {
        private readonly IContactRepository contactRepository;
        public DeleteContactInteractor(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }
        public async Task<Unit> Handle(DeleteContactRequest request, CancellationToken cancellationToken)
        {
            await contactRepository.DeleteAsync(request.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
