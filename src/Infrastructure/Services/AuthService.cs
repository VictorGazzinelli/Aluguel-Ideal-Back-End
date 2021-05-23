using AluguelIdeal.Application.Repositories;
using AluguelIdeal.Application.Services;
using AluguelIdeal.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AluguelIdeal.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRoleRepository roleRepository;
        private readonly JwtSecurityTokenHandler bearerTokenHandler;
        private readonly SymmetricSecurityKey key;

        public AuthService(IConfiguration configuration, IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
            byte[] base64DecodedSecretBytes = 
                Base64UrlTextEncoder.Decode(configuration.GetValue<string>("Secret"));
            this.key = new SymmetricSecurityKey(base64DecodedSecretBytes);
            this.bearerTokenHandler = new JwtSecurityTokenHandler();
        }

        public async Task<(string bearerToken, int expiresIn)> CreateBearerTokenAsync(string userEmail, CancellationToken cancellationToken = default)
        {
            int expireAfterHours = 8;
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddHours(expireAfterHours),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Subject = await GetUserClaimsIdentity(userEmail, cancellationToken)
            };
            SecurityToken token = bearerTokenHandler.CreateToken(tokenDescriptor);

            return(bearerTokenHandler.WriteToken(token), expireAfterHours * 3600);
        }

        private async Task<ClaimsIdentity> GetUserClaimsIdentity(string userEmail, CancellationToken cancellationToken) =>
            new ClaimsIdentity(await GetUserClaims(userEmail, cancellationToken));

        private async Task<IEnumerable<Claim>> GetUserClaims(string userEmail, CancellationToken cancellationToken)
        {
            List<Claim> userClaims = new List<Claim>(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userEmail)
            });
            foreach (Role userRole in await roleRepository.ReadByUserEmailAsync(userEmail, cancellationToken))
                userClaims.Add(new Claim(ClaimTypes.Role, userRole.Name));

            return userClaims;
        }
    }
}
