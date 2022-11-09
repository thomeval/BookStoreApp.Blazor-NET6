using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

public class BaseHttpService
{
    protected readonly IClient Client;
    protected readonly ILocalStorageService LocalStorage;

    public BaseHttpService(IClient client, ILocalStorageService localStorage)
    {
        Client = client;
        LocalStorage = localStorage;
    }

    protected Response<T> ConvertApiExceptions<T>(ApiException apiException)
    {
        switch (apiException.StatusCode)
        {
            case 400:
                return GetResponse<T>("Validation errors have occurred.", apiException.Response);
            case 401:
                return GetResponse<T>("The user is not logged in.");
            case 403:
                return GetResponse<T>("The user is not authorized to perform this action.");
            case 404:
                return GetResponse<T>("The requested item could not be found.");
            default:
                return GetResponse<T>("Something went wrong");
        }
    }

    private Response<T> GetResponse<T>(string message, string validationErrors = "", bool success = false)
    {
        return new Response<T> { Message = message, ValidationErrors = validationErrors, Success = success };
    }

    protected async Task GetBearerToken()
    {
        var token = await LocalStorage.GetItemAsync<string>("accessToken");
        if (token != null)
        {
            Client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
    }

    public string BaseUrl
    {
        get
        {
            return Client.HttpClient.BaseAddress!.ToString();
        }
    }
}
