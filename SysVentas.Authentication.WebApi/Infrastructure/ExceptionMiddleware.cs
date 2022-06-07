using System.Net;
using SysVentas.Authentication.Application.Base;
using SysVentas.Authentication.Infrastructure.Ldap.Base;
namespace SysVentas.Authentication.WebApi.Infrastructure;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (SysVentasApplicationException exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
        catch (SysVentasInfrastructureLdapException exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext);
            _logger.LogError(ex.Message);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return context.Response.WriteAsync(new ErrorValidation()
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
        }.ToString());
    }

    private static Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(new ErrorValidation
        {
            StatusCode = context.Response.StatusCode,
            Message = "There was an unexpected error"
        }.ToString());
    }
}
