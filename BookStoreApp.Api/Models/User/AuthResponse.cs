namespace BookStoreApp.Api.Models.User;

#nullable disable
public class AuthResponse
{
    public string UserId { get; set; }
    public string Token { get; set; }
    public string Email { get; set; }
}
