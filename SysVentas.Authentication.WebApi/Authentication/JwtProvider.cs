using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using SysVentas.Authentication.Application.TokenProvider;
using SysVentas.Authentication.Domain.Models;
namespace SysVentas.Authentication.WebApi.Authentication
{
    public class JwtProvider : ITokenProvider
    {
        private readonly RsaSecurityKey _key;
        private readonly string _algorithm;
        private readonly string _issuer;
        private readonly string _audience;
        public JwtProvider(string issuer, string audience)
        {
            var provider = new RSACryptoServiceProvider(2048);
            _key = new RsaSecurityKey(provider);
            _algorithm = SecurityAlgorithms.RsaSha256Signature;
            _issuer = issuer;
            _audience = audience;
        }
        public string CreateToken(User user, DateTime expiry)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity(new List<Claim>()
                {
                    new(ClaimTypes.Name, $"{user.Nombre}"),
                    new(ClaimTypes.Email, $"{user.Email}"),
                    new(ClaimTypes.Role, "Administrador"),
                    new(ClaimTypes.PrimarySid, user.Id.ToString())
                }, "Custom"
            );
            SecurityToken token = tokenHandler.CreateJwtSecurityToken(
                new SecurityTokenDescriptor
                {
                    Audience = _audience,
                    Issuer = _issuer,
                    SigningCredentials = new SigningCredentials(_key, _algorithm),
                    Expires = expiry.ToUniversalTime(),
                    Subject = identity
                });
            user.SetToken(tokenHandler.WriteToken(token));
            return user.Token;
        }

        public TokenProviderValidationParameters GetValidationParameters()
        {
            return new TokenProviderValidationParameters(
                _key,
                _audience,
                _issuer,
                true,
                TimeSpan.FromSeconds(0)
            );
        }
    }
}
