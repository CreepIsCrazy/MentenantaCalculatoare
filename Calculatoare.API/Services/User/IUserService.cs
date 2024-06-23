using Calculatoare.Data.Models;
using Calculatoare.Shared;
using Calculatoare.Shared.Authentication.DTOs;

namespace Calculatoare.API.Services.User
{
    public interface IUserService
    {
        Task<ServiceResponse<IEnumerable<user>>> GetUsers();
        Task<ServiceResponse<int>> RegisterUser(RegisterDTO user);
    }
}