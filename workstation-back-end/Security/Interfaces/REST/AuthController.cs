using Microsoft.AspNetCore.Mvc;
using workstation_back_end.Security.Domain.Models.Commands;
using workstation_back_end.Security.Domain.Services;
using workstation_back_end.Security.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Authorization;
namespace workstation_back_end.Security.Interfaces.REST;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
    {
        var user = await _authService.SignUpAsync(command);
        return Ok(new { message = "Usuario registrado con éxito", user.Email });
    }
    [AllowAnonymous]
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
    {
        var token = await _authService.SignInAsync(command);
        return Ok(new AuthResponseResource
        {
            Token = token,
            Email = command.Email,
            Rol = "usuario"
        });
    }
}