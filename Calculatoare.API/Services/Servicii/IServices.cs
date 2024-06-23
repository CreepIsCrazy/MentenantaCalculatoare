using Calculatoare.Shared;
using Calculatoare.Shared.Order;

namespace Calculatoare.API.Services.Servicii;
public interface IServices
{
    Task AddOrEditService(Item item);
    Task<ServiceResponse<Item>> GetService(int id);
    Task<ServiceResponse<IEnumerable<Item>>> GetServices();
}