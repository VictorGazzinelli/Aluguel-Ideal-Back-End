using MediatR;
using System;

namespace AluguelIdeal.Application.Interactors.Residences.Commands
{
    public class DeleteResidenceCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
