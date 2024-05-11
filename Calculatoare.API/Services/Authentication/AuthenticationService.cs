using Calculatoare.Data.Context;
using Calculatoare.Data.Models;
using Calculatoare.Shared;
using Calculatoare.Shared.Authentication.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Calculatoare.API.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly CalculatoareContext _context;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(CalculatoareContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<string>> Login(LoginDTO login)
    {
        var user = await _context.user.FirstOrDefaultAsync(x => x.Username == login.Username);
        if (user == null)
            return new ServiceResponse<string>
            {
                Success = false,
                Message = "User not found."
            };
        else if (login.Password != user.Password)
            return new ServiceResponse<string>
            {
                Success = false,
                Message = "Wrong password."
            };
        else if (!user.IsActive)
            return new ServiceResponse<string>
            {
                Success = false,
                Message = "This account has been deactivated. Please contact an administrator."
            };

        return new ServiceResponse<string>
        {
            Data = CreateToken(user)
        };
    }

    public async Task<ServiceResponse<string>> Register(RegisterDTO register)
    {
        if (await UserExists(register.Username))
            return new ServiceResponse<string>
            {
                Success = false,
                Message = "User already exists."
            };

        if (await EmailExists(register.Email))
            return new ServiceResponse<string>
            {
                Success = false,
                Message = "Email already in use."
            };

        await _context.user.AddAsync(new user
        {
            Username = register.Username,
            Password = register.Password,
            Email = register.Email,
            DateCreated = DateTime.Now,
            LastModified = DateTime.Now,
            IsActive = true,
            TypeId = (int)register.Type,

            Details = register.Firstname != null &&
                      register.Lastname != null ? 
                new user_detail
                {
                    Firstname = register.Firstname,
                    Lastname = register.Lastname,
                    Phone = register.Phone,
                    Adress = register.Adress,
                    City = register.City,
                    Country = register.Country,
                    ZipCode = register.ZipCode
                } : null
        });
        await _context.SaveChangesAsync();

        return new ServiceResponse<string>
        {
            Data = register.Username,
            Message = "Registration successfull."
        };
    }

    public async Task<ServiceResponse<bool>> ChangePassword(ChangePasswordDTO change)
    {
        var user = await _context.user.FindAsync(change.UserId);
        if (user is null)
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "User not found."
            };

        user.Password = change.NewPassword;

        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
            Data = true,
            Message = "Password has been changed."
        };
    }

    #region Helper Methods
    public async Task<bool> UserExists(string username) =>
        await _context.user.AnyAsync(user => user.Username == username);

    public async Task<bool> EmailExists(string email) =>
        email is not null && await _context.user.AnyAsync(x => x.Email.ToLower() == email.ToLower());

    private string CreateToken(user user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.TypeId.ToString()),
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JWTToken").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(5),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    #region Get User information
    public int GetUserId() =>
        int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

    public string GetUserUsername() =>
        _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
    #endregion
    #endregion  
}
