using System.Runtime.Serialization;
namespace SysVentas.Authentication.Infrastructure.Ldap.Base;

[Serializable]
public class SysVentasInfrastructureLdapException : Exception
{
    public SysVentasInfrastructureLdapException() { }
    public SysVentasInfrastructureLdapException(string message) : base(message) { }
    public SysVentasInfrastructureLdapException(string message, Exception inner) : base(message, inner) { }
    protected SysVentasInfrastructureLdapException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}