using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using SysVentas.Authentication.Domain.Services;
using SysVentas.Authentication.Infrastructure.Ldap.Base;
namespace SysVentas.Authentication.Infrastructure.Ldap;

public class ValidateUserAdService : IValidateUserAdService
{
    private readonly IBuildDistinguishedNameService _buildDistinguishedNameService;
    private readonly LdapConnectionParameters _ldapConnectionParameters;

    public ValidateUserAdService(IBuildDistinguishedNameService buildDistinguishedNameService, IOptions<LdapConnectionParameters> options)
    {
        _buildDistinguishedNameService = buildDistinguishedNameService;
        _ldapConnectionParameters = options.Value;
    }
    public bool Handle(string commonName, string password)
    {
        try
        {
            var distinguishedName = _buildDistinguishedNameService.Handle(commonName);
            using var connection = new LdapConnection
            {
                SecureSocketLayer = false
            };
            connection.Connect(_ldapConnectionParameters.Host, _ldapConnectionParameters.Port);
            connection.Bind(distinguishedName, password);
            return connection.Bound;
        }
        catch (Exception exception)
        {
            throw new SysVentasInfrastructureLdapException(exception.Message);
        }
    }
}
