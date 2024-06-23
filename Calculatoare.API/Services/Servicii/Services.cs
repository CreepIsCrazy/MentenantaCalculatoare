using Calculatoare.Data.Context;
using Calculatoare.Data.Models;
using Calculatoare.Shared;
using Calculatoare.Shared.Order;
using Microsoft.EntityFrameworkCore;

namespace Calculatoare.API.Services.Servicii;

public class Services(CalculatoareContext _ctx) : IServices
{
    public async Task<ServiceResponse<IEnumerable<Item>>> GetServices()
    {
        var services = await _ctx.service.AsNoTracking()
            .Select(x => new Item
            {
                ItemId = x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                PictureURL = x.PictureURL
            }).ToListAsync();

        return new ServiceResponse<IEnumerable<Item>>
        {
            Data = services
        };
    }
}
