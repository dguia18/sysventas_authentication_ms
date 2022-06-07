using Microsoft.EntityFrameworkCore;
using SysVentas.Authentication.Domain.Models;
namespace SysVentas.Authentication.Infrastructure.Data;

public class AuthenticationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public static string DefaultSchema => "Authentication";
    public AuthenticationContext(DbContextOptions options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable(nameof(User),DefaultSchema);
            builder.HasIndex(t => t.Id);
            builder.Property(t => t.Email).IsRequired();
            builder.Property(t => t.Nombre).IsRequired();
            builder.HasIndex(t => t.Email).IsUnique();
            builder.Property(t => t.Password).IsRequired();
        });
    }
}
