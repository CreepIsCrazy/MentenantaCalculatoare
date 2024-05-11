using Calculatoare.API.Services.Authentication;
using Calculatoare.Shared;
using Calculatoare.Shared.Authentication.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calculatoare.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<string>>> Login(LoginDTO login)
    {
        var response = await _authService.Login(login);
        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<string>>> Register(RegisterDTO register)
    {
        var response = await _authService.Register(register);
        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("change-password"), Authorize]
    public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword(ChangePasswordDTO change)
    {
        var response = await _authService.ChangePassword(change);
        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }
}
