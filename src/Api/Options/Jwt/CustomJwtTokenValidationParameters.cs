using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace AluguelIdeal.Api.Options.Jwt
{
    public class CustomJwtTokenValidationParameters : TokenValidationParameters
    {
        public CustomJwtTokenValidationParameters(string secret)
        {
            this.ValidateAudience = false;
            this.ValidateIssuer = false;
            this.IssuerSigningKey = new SymmetricSecurityKey(Base64UrlTextEncoder.Decode(secret));
        }
    }
}
