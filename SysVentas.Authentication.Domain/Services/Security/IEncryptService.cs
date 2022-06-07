using System.Security.Cryptography;
using System.Text;
namespace SysVentas.Authentication.Domain.Services.Security;

public interface IEncryptService
{
    string Handle(string str);
}
public class EncryptWithSha256Service : IEncryptService
{
    public string Handle(string str)
    {
        var sha256 = SHA256.Create();
        var encoding = new ASCIIEncoding();
        var sb = new StringBuilder();
        var stream = sha256.ComputeHash(encoding.GetBytes(str));
        foreach (var t in stream)
            sb.Append($"{t:x2}");
        return sb.ToString();
    }
}
