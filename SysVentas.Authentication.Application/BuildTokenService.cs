using SysVentas.Authentication.Domain.Models.Services;
using SysVentas.Authentication.Domain.Services;
namespace SysVentas.Authentication.Application;

public class BuildTokenService : IBuildTokenService
{
    private readonly IValidateUserAdService _validateUserAdService;
    private readonly IUserRepository _userRepository;
    public BuildTokenService(IValidateUserAdService validateUserAdService, IUserRepository userRepository)
    {
        _validateUserAdService = validateUserAdService;
        _userRepository = userRepository;
    }
    public IBuildTokenService.Response Handle(IBuildTokenService.Request request)
    {
        _validateUserAdService.Handle(request.Email, request.Password);
        var user = _userRepository.FindFirstOrDefault(t => t.Email == request.Email && t.Password == request.Password);
        if (user == null)
        {
            throw new ApplicationException($"Usuario con {request.Email} no fue localizado.");
        }
        return new IBuildTokenService.Response("El token");
    }
}
