using BookStoreApp.Blazor.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Blazor.Server.UI.Pages.Users;

public partial class Register
{

    private readonly UserDto _registrationModel = new UserDto{Role = "User"};

    [Inject] private IClient _httpClient { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;

    private string _message = string.Empty;
    private async Task HandleRegistration()
    {
        try
        {
            await _httpClient.RegisterAsync(_registrationModel);
            _navigationManager.NavigateTo("/users/login");
        }
        catch (ApiException<ProblemDetails> ex)
        {
            _message = ex.Message;
            foreach (var prop in ex.Result.AdditionalProperties)
            {
                _message += $"{prop.Value}\r\n";
            }
        }

        catch (Exception ex)
        {
            _message = ex.Message;
        }
   
    }
}
