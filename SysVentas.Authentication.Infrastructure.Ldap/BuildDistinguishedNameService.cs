using Microsoft.Extensions.Options;
using SysVentas.Authentication.Domain.Services;
namespace SysVentas.Authentication.Infrastructure.Ldap;

public class BuildDistinguishedNameService : IBuildDistinguishedNameService
{
    private readonly LdapParameters _ldapParameters;
    public BuildDistinguishedNameService(IOptions<LdapParameters> options)
    {
        _ldapParameters = options.Value;
    }
    public string Handle(string commonName)
    {
        var domainSplit = _ldapParameters.Domain.Split(".");
        var domainsComponents = string.Join(",", domainSplit.Select(t => $"dc={t}"));
        return $"cn={commonName},ou={_ldapParameters.OrganizationalUnit},{domainsComponents}";
    }
}
