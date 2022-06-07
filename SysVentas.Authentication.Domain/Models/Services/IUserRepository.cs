namespace SysVentas.Authentication.Domain.Models.Services;

public interface IUserRepository
{
    User FindFirstOrDefault(Func<User, bool> predicate);
    void CommitChanges();
}
