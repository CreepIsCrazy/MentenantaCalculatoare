using Calculatoare.Data.Context;
using Calculatoare.Data.Models;
using Calculatoare.Shared;
using Microsoft.EntityFrameworkCore;

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
}
