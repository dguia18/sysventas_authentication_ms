namespace SysVentas.Authentication.Domain.Services;

public interface IBuildDistinguishedNameService
{
    string Handle(string commonName);
}