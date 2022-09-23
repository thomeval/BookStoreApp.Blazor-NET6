using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BookStoreApp.Api.Data;

public class ApiUser : IdentityUser
{
    [StringLength(100)]
    public string? FirstName { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

}
