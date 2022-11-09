using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Blazor.WebAssembly.UI.Providers;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
    private readonly ClaimsPrincipal _noUserPrincipal = new(new ClaimsIdentity());

    public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    private async Task<ClaimsPrincipal> GetClaimsPrincipalAsync()
    {
        var savedToken = await _localStorage.GetItemAsync<string>("accessToken");

        if (savedToken == null)
        {
            return _noUserPrincipal;
        }

        var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);

        if (tokenContent.ValidTo < DateTime.UtcNow || tokenContent.ValidFrom > DateTime.UtcNow)
        {
            await _localStorage.RemoveItemAsync("accessToken");
            return _noUserPrincipal;
        }

        var claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        return user;

    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return new AuthenticationState(await GetClaimsPrincipalAsync());
    }

    public void LoggedIn()
    {
        var authState = GetAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task LoggedOut()
    {
        var authState = Task.FromResult(new AuthenticationState(_noUserPrincipal));
        await _localStorage.RemoveItemAsync("accessToken");
        NotifyAuthenticationStateChanged(authState);
    }
}
