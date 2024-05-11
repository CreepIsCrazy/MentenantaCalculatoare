using Calculatoare.Shared.User;

namespace Calculatoare.Shared.Authentication.DTOs;

public class RegisterDTO
{
    public string Username { get; set; }
    public string Password { get; set; }

    public string Email { get; set; }

    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Phone { get; set; }
    public string Adress { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }

    public UserType Type { get; set; }
}
