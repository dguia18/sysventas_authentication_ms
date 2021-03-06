namespace SysVentas.Authentication.Application;

public interface IBuildTokenService
{
    Response Handle(Request request);
    public record Response(string Token,int ExpiresIn);
    public class Request
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}