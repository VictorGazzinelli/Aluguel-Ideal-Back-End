using AluguelIdeal.Application.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.AspNetCore.Authentication;

namespace AluguelIdeal.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;
        private readonly JwtSecurityTokenHandler bearerTokenHandler;

        public AuthService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.bearerTokenHandler = new JwtSecurityTokenHandler();
        }

        public string CreateBearerToken()
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Base64UrlTextEncoder.Decode(configuration.GetValue<string>("Secret")));
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            };
            SecurityToken token = bearerTokenHandler.CreateToken(tokenDescriptor);

            return bearerTokenHandler.WriteToken(token);
        }
    }
}
