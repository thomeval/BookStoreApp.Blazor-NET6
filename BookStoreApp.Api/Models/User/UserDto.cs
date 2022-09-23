using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Api.Models.User;

#nullable disable
public class UserDto : UserLoginDto
{

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Role { get; set; }
}