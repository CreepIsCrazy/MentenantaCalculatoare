using Calculatoare.Data.Context;
using Calculatoare.Data.Models;
using Calculatoare.Shared;
using Calculatoare.Shared.Authentication.DTOs;
using Calculatoare.Shared.User;
using Microsoft.EntityFrameworkCore;
using System;

namespace Calculatoare.API.Services.User;

public class UserService : IUserService
{
    private readonly CalculatoareContext _context;

    public UserService(CalculatoareContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<IEnumerable<user>>> GetUsers()
    {
        var users = await _context.user.AsNoTracking()
            .Where(x => x.IsActive)
            .ToListAsync();

        return new ServiceResponse<IEnumerable<user>> { Data = users };
    }

    public async Task<ServiceResponse<int>> RegisterUser(RegisterDTO user)
    {
        try
        {
            var userModel = new user
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                DateCreated = DateTime.Now,
                LastModified = DateTime.Now,
                IsActive = true,
                TypeId = (int)user.Type,
                Details = new user_detail
                {
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Phone = user.Phone,
                    Adress = user.Adress,
                    City = user.City,
                    Country = user.Country,
                    ZipCode = user.ZipCode,
                }
            };
            _context.user.Add(userModel);
            await _context.SaveChangesAsync();
            return new ServiceResponse<int> { Data = userModel.Id };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<int> { Message = ex.Message, Success = false };
        }
    }
}
