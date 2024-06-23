using Calculatoare.API.Services.Servicii;
using Calculatoare.API.Services.User;
using Calculatoare.Data.Models;
using Calculatoare.Shared;
using Calculatoare.Shared.Authentication.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calculatoare.API.Controllers;

[Route("[controller]")]
[ApiController]
public class ServicesController(IServices _services) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<user>>>> GetUsers() => 
        await _userService.GetUsers();

    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<int>>> RegisterUser(RegisterDTO user)
    {
        var response = await _userService.RegisterUser(user);
        return Ok(response);
    }
}
