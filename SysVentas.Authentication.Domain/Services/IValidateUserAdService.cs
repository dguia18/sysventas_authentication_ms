namespace SysVentas.Authentication.Domain.Services;

public interface IValidateUserAdService
{
    bool Handle(string commonName, string password);
}