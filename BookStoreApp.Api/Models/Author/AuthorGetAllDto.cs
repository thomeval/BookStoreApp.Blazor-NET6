using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Api.Models.Author;

public class AuthorGetAllDto
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(50)]
    public string LastName { get; set; }

    [StringLength(250)]
    public string Bio { get; set; }

}
