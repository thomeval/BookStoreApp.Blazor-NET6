using Blazored.LocalStorage;
using BookStoreApp.Blazor.WebAssembly.UI.Providers;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly ApiAuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, ApiAuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> AuthenticateAsync(UserLoginDto loginModel)
    {
        var response = await _httpClient.LoginAsync(loginModel);

        // Store Token
        await _localStorage.SetItemAsync("accessToken", response.Token);

        // Change auth state of app
        _authenticationStateProvider.LoggedIn();

        return true;
    }

    public async Task Logout()
    {
        await _authenticationStateProvider.LoggedOut();
    }
}