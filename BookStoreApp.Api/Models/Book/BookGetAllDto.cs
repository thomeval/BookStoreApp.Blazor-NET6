using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Api.Models.Book;

#nullable disable
public class BookGetAllDto : BaseDto
{
    [Required]
    [StringLength(50)]
    public string Title { get; set; }
    public string Image { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public decimal Price { get; set; }

    public int? AuthorId { get; set; }
    public string AuthorName { get; set; }
}
