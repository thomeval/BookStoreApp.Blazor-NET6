using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication;

public interface IAuthenticationService
{
    Task<Response<AuthResponse>> AuthenticateAsync(UserLoginDto loginModel);
    Task Logout();
}