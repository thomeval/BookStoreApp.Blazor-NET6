using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models.Author;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Api.Repositories;

public class AuthorsRepository : GenericRepository<Author>, IAuthorsRepository
{

    public AuthorsRepository(BookStoreDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<AuthorDetailsDto?> GetAuthorDetailsAsync(int id)
    {
        var author = await DbContext.Authors
            .Include(e => e.Books)
            .ProjectTo<AuthorDetailsDto>(Mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(e => e.Id == id);
        return author;
    }
}