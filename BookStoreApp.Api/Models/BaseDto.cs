using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Api.Models;

#nullable disable

public class BaseDto
{
    [Required]
    public int Id { get; set; }
}
