using SysVentas.Authentication.Domain.Models;
using SysVentas.Authentication.Domain.Models.Services;
namespace SysVentas.Authentication.Infrastructure.Data;

public class UserRepository: IUserRepository
{
    private readonly AuthenticationContext _dbContext;

    public UserRepository(AuthenticationContext dbContext)
    {
        _dbContext = dbContext;
    }
    public User FindFirstOrDefault(Func<User, bool> predicate)
    {
        return _dbContext.Users.FirstOrDefault(predicate)!;
    }
}
