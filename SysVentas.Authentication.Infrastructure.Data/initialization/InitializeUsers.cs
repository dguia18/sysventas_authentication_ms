using SysVentas.Authentication.Domain.Models;
using SysVentas.Authentication.Domain.Services.Security;
namespace SysVentas.Authentication.Infrastructure.Data.initialization;

public static class InitializeUsers
{
    public static void InitializeUser(this AuthenticationContext context)
    {
        var encrypt = new EncryptWithSha256Service();
        if (context.Users.Any()) return;
        var userDuvan = new User()
        {
            Email = "dguia@unicesar.edu.co",
            Nombre = "Duvan Felipe Guia",
            Password = encrypt.Handle("123")
        };
        context.Users.Add(userDuvan);
        var userJeisson = new User()
        {
            Email = "javergarav@unicesar.edu.co",
            Nombre = "Jeisson Andrés Vergara Vargas",
            Password = encrypt.Handle("123")
        };
        context.Users.Add(userJeisson);
        var userIvan = new User()
        {
            Email = "idcontreras@unicesar.edu.co",
            Nombre = "Ivan Dario Contreras Julio",
            Password = encrypt.Handle("ivancontry123")
        };
        context.Users.Add(userIvan);
        var userIsmael = new User()
        {
            Email = "aism793@unicesar.edu.co",
            Nombre = "Ismael Centeno",
            Password = encrypt.Handle("ismael321")
        };
        context.Users.Add(userIsmael);
        var userMicheel = new User()
        {
            Email = "micheel@unicesar.edu.co",
            Nombre = "Micheel",
            Password = encrypt.Handle("micheel123")
        };
        context.Users.Add(userMicheel);
        var userIvanRios = new User()
        {
            Email = "ivanrios@unicesar.edu.co",
            Nombre = "Ivan Dario Rios",
            Password = encrypt.Handle("ivanRios123")
        };
        context.Users.Add(userIvanRios);
        var userYair = new User()
        {
            Email = "yairvargas@unicesar.edu.co",
            Nombre = "Yair Vargas Delgado",
            Password = encrypt.Handle("yairVargas123")
        };
        context.Users.Add(userYair);
        
        context.SaveChanges();
    }
}
