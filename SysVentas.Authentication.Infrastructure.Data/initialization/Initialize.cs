namespace SysVentas.Authentication.Infrastructure.Data.initialization;

public class Initialize
{
    private readonly AuthenticationContext _context;
    public Initialize(AuthenticationContext context)
    {
        _context = context;
    }
    public void Handle()
    {
        _context.InitializeUser();
    }
}
