using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.Api.Controllers;
using BookStoreApp.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Api.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly BookStoreDbContext DbContext;
    protected readonly IMapper Mapper;

    public GenericRepository(BookStoreDbContext dbContext, IMapper mapper)
    {
        DbContext = dbContext;
        Mapper = mapper;
    }

    public async Task<T?> GetAsync(int id)
    {
        return await DbContext.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await DbContext.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {

        await DbContext.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        DbContext.Update(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetAsync(id);

        if (entity == null)
        {
            return false;
        }

        DbContext.Set<T>().Remove(entity);
        await DbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await GetAsync(id);
        return entity != null;
    }

    public async Task<VirtualizeResponse<TResult>> GetAllAsync<TResult>(QueryParameters param) where TResult : class
    {
        var totalSize = await DbContext.Set<T>().CountAsync();

        var items = await DbContext.Set<T>()
            .Skip(param.StartIndex)
            .Take(param.PageSize)
            .ProjectTo<TResult>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return new VirtualizeResponse<TResult>{Items = items, TotalSize = totalSize};
    }
}