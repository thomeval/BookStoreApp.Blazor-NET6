using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models.Book;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Api.Repositories;

public class BooksRepository : GenericRepository<Book>, IBooksRepository
{

    public BooksRepository(BookStoreDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<BookGetSingleDto?> GetBookAsync(int id)
    {
        var book = await DbContext.Books
            .Include(b => b.Author)
            .ProjectTo<BookGetSingleDto>(Mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(e => e.Id == id);
        return book;
    }

    public async Task<List<BookGetAllDto>> GetAllBooksAsync()
    {
        var books = await DbContext.Books
            .Include(b => b.Author)
            .ProjectTo<BookGetAllDto>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return books;
    }
}