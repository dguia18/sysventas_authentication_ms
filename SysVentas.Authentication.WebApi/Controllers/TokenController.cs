using Microsoft.AspNetCore.Mvc;
using SysVentas.Authentication.Application;
namespace SysVentas.Authentication.WebApi.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IBuildTokenService _buildTokenService;
    public TokenController(IBuildTokenService buildTokenService)
    {
        _buildTokenService = buildTokenService;
    }
    [HttpGet]
    public IActionResult Get([FromQuery] IBuildTokenService.Request request)
    {
        var response = _buildTokenService.Handle(request);
        return Ok(new
        {
            AccessToken = response.Token,
            ExpiresIn = response.ExpiresIn
        });
    }
}
