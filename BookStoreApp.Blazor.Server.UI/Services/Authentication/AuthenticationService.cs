using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Providers;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication;

public class AuthenticationService : BaseHttpService, IAuthenticationService
{
    private readonly IClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly ApiAuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, ApiAuthenticationStateProvider authenticationStateProvider) : base(httpClient, localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<Response<AuthResponse>> AuthenticateAsync(UserLoginDto loginModel)
    {
        try
        {
            var result = await _httpClient.LoginAsync(loginModel);

            var response = new Response<AuthResponse>
            {
                Data = result,
                Success = true
            };

            // Store Token
            await _localStorage.SetItemAsync("accessToken", result.Token);

            // Change auth state of app
            _authenticationStateProvider.LoggedIn();

            
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<AuthResponse>(ex);
        }

    }

    public async Task Logout()
    {
        await _authenticationStateProvider.LoggedOut();
    }
}