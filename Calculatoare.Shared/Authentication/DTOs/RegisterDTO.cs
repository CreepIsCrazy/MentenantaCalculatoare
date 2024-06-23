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
/*@code {
    LoginDTO user = new LoginDTO();

async Task HandleLogin()
{
    var result = await Http.PostAsJsonAsync("Authentication/login", user);
    var token = await result.Content.ReadAsStringAsync();
    //Console.WriteLine(token);
    await LocalStorage.SetItemAsync("token", token);
    var state = await AuthStateProvider.GetAuthenticationStateAsync();
    if (state is not null)
    {
        foreach (var item in state.User.Claims)
        {
            Console.WriteLine($"{item.Type}, {item.Value}");
        }
        var roleClaim = state.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Role);
        if (roleClaim is not null &&
            int.TryParse(roleClaim.Value, out int roleId) &&
            roleId == 1)
            _nav.NavigateTo("/dashboard");
    }
}
}*/