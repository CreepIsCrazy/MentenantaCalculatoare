using Calculatoare.Shared;
using Calculatoare.Shared.Authentication.DTOs;

namespace Calculatoare.API.Services.Authentication;
public interface IAuthenticationService
{
    Task<ServiceResponse<bool>> ChangePassword(ChangePasswordDTO change);
    Task<bool> EmailExists(string email);
    int GetUserId();
    string GetUserUsername();
    Task<ServiceResponse<string>> Login(LoginDTO login);
    Task<ServiceResponse<string>> Register(RegisterDTO register);
    Task<bool> UserExists(string username);
}