using Calculatoare.API.Services.Servicii;
using Calculatoare.Shared;
using Calculatoare.Shared.Order;
using Microsoft.AspNetCore.Mvc;

namespace Calculatoare.API.Controllers;

[Route("[controller]")]
[ApiController]
public class ServicesController(IServices _services) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<Item>>>> GetServices() => 
        await _services.GetServices();

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Item>>> GetService(int id) =>
        await _services.GetService(id);

    [HttpPost("AddOrEdit")]
    public async Task<ActionResult> AddOrEditService(Item item)
    {
        await _services.AddOrEditService(item);
        return Created();
    }
}
