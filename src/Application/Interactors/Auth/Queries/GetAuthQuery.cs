using AluguelIdeal.Application.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AluguelIdeal.Application.Interactors.Auth.Queries
{
    public class GetAuthQuery : IRequest<AuthDto>
    {
        [ModelBinder(Name = "grant_type")]
        public string GrantType { get; set; }
        [ModelBinder(Name = "username")]
        public string Email { get; set; }
        [ModelBinder(Name = "password")]
        public string Password { get; set; }
        [ModelBinder(Name = "client_id")]
        public string ClientId { get; set; }
    }
}
