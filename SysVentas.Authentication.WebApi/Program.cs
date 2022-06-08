using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using SysVentas.Authentication.Application;
using SysVentas.Authentication.Application.TokenProvider;
using SysVentas.Authentication.Domain.Models.Services;
using SysVentas.Authentication.Domain.Services;
using SysVentas.Authentication.Domain.Services.Security;
using SysVentas.Authentication.Infrastructure.Data;
using SysVentas.Authentication.Infrastructure.Data.initialization;
using SysVentas.Authentication.Infrastructure.Ldap;
using SysVentas.Authentication.WebApi.Authentication;
using SysVentas.Authentication.WebApi.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddEnvironmentVariables();
builder.Services.Configure<LdapParameters>(opt =>
    builder.Configuration.GetSection("LdapParameters").Bind(opt)
);
builder.Services.Configure<LdapConnectionParameters>(opt =>
    builder.Configuration.GetSection("LdapParametersConnection").Bind(opt)
);

builder.Services.AddDbContext<AuthenticationContext>((_, optionsBuilder) => optionsBuilder
    .UseSqlServer(builder.Configuration["ConnectionString"]));

builder.Services.AddScoped<IValidateUserAdService, ValidateUserAdService>();
builder.Services.AddScoped<IBuildDistinguishedNameService, BuildDistinguishedNameService>();
builder.Services.AddScoped<IBuildTokenService, BuildTokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEncryptService, EncryptWithSha256Service>();

var tokenProvider = new JwtProvider("issuer", "audience");
builder.Services.AddSingleton<ITokenProvider>(tokenProvider);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = tokenProvider.GetValidationParametersToTokenValidation();
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<AuthenticationContext>();
context.Database.Migrate();
var initialize = new Initialize(context);
initialize.Handle();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
