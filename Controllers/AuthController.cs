using AeromexicoPrueba.Contracts.Requests;
using AeromexicoPrueba.Services;
using Microsoft.AspNetCore.Mvc;

namespace AeromexicoPrueba.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var response = _authService.Login(request);
        return response.Authorized ? Ok(response) : Unauthorized(response);
    }
}
