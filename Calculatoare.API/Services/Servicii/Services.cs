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

    public async Task<ServiceResponse<Item>> GetService(int id)
    {
        var service = await _ctx.service.AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new Item
            {
                ItemId = x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                PictureURL = x.PictureURL
            }).FirstOrDefaultAsync();
        return new ServiceResponse<Item>
        {
            Data = service
        };
    }

    public async Task RemoveService(int id)
    {
        var service = await _ctx.service.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (service is null)
            throw new Exception("service not found");

        _ctx.service.Remove(service);
        await _ctx.SaveChangesAsync();
    }

    public async Task AddOrEditService(Item item)
    {
        if (item.ItemId == 0)
            await _ctx.service.AddAsync(new service
            {
                Title = item.Title,
                Description = item.Description,
                Price = item.Price
            });
        else
        {
            var service = await _ctx.service.FirstOrDefaultAsync(x => x.Id == item.ItemId);
            if (service != null)
            {
                service.Title = item.Title;
                service.Description = item.Description;
                service.Price = item.Price;
            }
        }

        await _ctx.SaveChangesAsync();
    }
}
