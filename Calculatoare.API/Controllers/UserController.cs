using Calculatoare.API.Services.User;
using Calculatoare.Data.Models;
using Calculatoare.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Calculatoare.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<user>>>> GetUsers() => 
        await _userService.GetUsers();
}
