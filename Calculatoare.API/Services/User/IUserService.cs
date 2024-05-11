using Calculatoare.Data.Models;
using Calculatoare.Shared;

namespace Calculatoare.API.Services.User
{
    public interface IUserService
    {
        Task<ServiceResponse<IEnumerable<user>>> GetUsers();
    }
}