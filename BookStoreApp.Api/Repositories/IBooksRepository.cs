using System.Threading.Tasks;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models.Book;

namespace BookStoreApp.Api.Repositories;

public interface IBooksRepository : IGenericRepository<Book>
{
    Task<List<BookGetAllDto>> GetAllBooksAsync();
    Task<BookGetSingleDto?> GetBookAsync(int id);
}