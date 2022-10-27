using System.ComponentModel.DataAnnotations;
using BookStoreApp.Api.Models.Book;

namespace BookStoreApp.Api.Models.Author;

public class AuthorDetailsDto : AuthorGetSingleDto
{
    
    public List<BookGetSingleDto> Books { get; set; }

}
