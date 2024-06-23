using Calculatoare.Shared;
using Calculatoare.Shared.Order;

namespace Calculatoare.API.Services.Servicii;
public interface IServices
{
    Task<ServiceResponse<IEnumerable<Item>>> GetServices();
}