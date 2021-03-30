using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Application.Interactors.Contacts.Handlers
{
    public class DeleteContactInteractor : IRequestHandler<DeleteContactRequest>
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
