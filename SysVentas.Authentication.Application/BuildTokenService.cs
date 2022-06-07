using SysVentas.Authentication.Application.Base;
using SysVentas.Authentication.Application.TokenProvider;
using SysVentas.Authentication.Domain.Models.Services;
using SysVentas.Authentication.Domain.Services;
using SysVentas.Authentication.Domain.Services.Security;
namespace SysVentas.Authentication.Application;

public class BuildTokenService : IBuildTokenService
{
    private readonly IValidateUserAdService _validateUserAdService;
    private readonly IUserRepository _userRepository;
    private readonly ITokenProvider _tokenProvider;
    private readonly IEncryptService _encryptService;
    public BuildTokenService(IValidateUserAdService validateUserAdService, IUserRepository userRepository, ITokenProvider tokenProvider, IEncryptService encryptService)
    {
        _validateUserAdService = validateUserAdService;
        _userRepository = userRepository;
        _tokenProvider = tokenProvider;
        _encryptService = encryptService;
    }
    public IBuildTokenService.Response Handle(IBuildTokenService.Request request)
    {
        _validateUserAdService.Handle(request.Email, request.Password);
        var user = _userRepository.FindFirstOrDefault(t => t.Email == request.Email &&
                                                           t.Password == _encryptService.Handle(request.Password));
        if (user == null)
        {
            throw new SysVentasApplicationException($"Usuario y contraseña no coinciden con un usuario registrado.");
        }
        var expiry = DateTime.UtcNow.AddHours(8);
        return new IBuildTokenService.Response(_tokenProvider.CreateToken(user, expiry), expiry.Millisecond);
    }
}
