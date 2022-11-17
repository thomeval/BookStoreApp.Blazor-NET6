﻿using System.Threading.Tasks;
using BookStoreApp.Api.Controllers;

namespace BookStoreApp.Api.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<bool> Exists(int id);

    Task<VirtualizeResponse<TResult>> GetAllAsync<TResult>(QueryParameters param) where TResult : class;
}