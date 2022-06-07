using SysVentas.Authentication.Domain.Models;
namespace SysVentas.Authentication.Application.TokenProvider
{
    public interface ITokenProvider
    {
        string CreateToken(User user, DateTime expiry);
        TokenProviderValidationParameters GetValidationParameters();
    }
    public record TokenProviderValidationParameters(object RsaSecurityKey, string Audience, string Issuer, bool ValidateLifeTime, TimeSpan ClockSkew);
}
