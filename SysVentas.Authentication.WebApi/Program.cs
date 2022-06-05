using Microsoft.EntityFrameworkCore;
using SysVentas.Authentication.Application;
using SysVentas.Authentication.Domain.Models.Services;
using SysVentas.Authentication.Domain.Services;
using SysVentas.Authentication.Infrastructure.Data;
using SysVentas.Authentication.Infrastructure.Ldap;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<LdapParameters>(builder.Configuration.GetSection("LdapParameters"));
builder.Services.Configure<LdapConnectionParameters>(builder.Configuration.GetSection("LdapParametersConnection"));

builder.Services.AddDbContext<AuthenticationContext>((_, optionsBuilder) => optionsBuilder
    .UseInMemoryDatabase("AuthenticationDb"));

builder.Services.AddScoped<IValidateUserAdService, ValidateUserAdService>();
builder.Services.AddScoped<IBuildDistinguishedNameService, BuildDistinguishedNameService>();
builder.Services.AddScoped<IBuildTokenService, BuildTokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
