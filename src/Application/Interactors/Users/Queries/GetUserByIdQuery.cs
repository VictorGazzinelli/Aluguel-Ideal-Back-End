using AluguelIdeal.Application.Dtos.Users;
using MediatR;
using System;

namespace AluguelIdeal.Application.Interactors.Users.Queries
{
    public class GetUserByIdQuery : IRequest<InsensitiveUserDto>
    {
        public Guid Id { get; set; }
    }
}
