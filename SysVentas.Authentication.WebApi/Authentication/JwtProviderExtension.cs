using Microsoft.IdentityModel.Tokens;
using SysVentas.Authentication.Application.TokenProvider;
namespace SysVentas.Authentication.WebApi.Authentication;

public static class JwtProviderExtension
{
    public static TokenValidationParameters GetValidationParametersToTokenValidation(this ITokenProvider jwtProvider)
    {
        var validators = jwtProvider.GetValidationParameters();
        return new TokenValidationParameters
        {
            IssuerSigningKey = validators.RsaSecurityKey as RsaSecurityKey,
            ValidAudience = validators.Audience,
            ValidIssuer = validators.Issuer,
            ValidateLifetime = validators.ValidateLifeTime,
            ClockSkew = validators.ClockSkew
        };
    }
}
