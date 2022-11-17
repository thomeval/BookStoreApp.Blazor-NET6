using BookStoreApp.Blazor.Server.UI.Services.Authentication;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Blazor.Server.UI.Pages.Users;

public partial class Login
{

    [Inject] private IAuthenticationService _authService { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;

    public UserLoginDto LoginModel = new();

    private string _message = string.Empty;

    public async Task HandleLogin()
    {
        var response = await _authService.AuthenticateAsync(LoginModel);

        if (response.Success)
        {
            _navigationManager.NavigateTo("/");
            return;
        }

        _message = response.Message ?? string.Empty;
    }
}
