using BookStoreApp.Blazor.WebAssembly.UI.Models;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services;

public interface IAuthorService
{
    Task<Response<AuthorDetailsDto>> Get(int id);
    Task<Response<AuthorGetAllDtoVirtualizeResponse>> GetAll(QueryParameters queryParams);
    Task<Response<List<AuthorGetAllDto>>> GetAll();
    Task<Response<int>> Create(AuthorCreateDto author);
    Task<Response<int>> Update(int id, AuthorUpdateDto author);
    Task<Response<int>> Delete(int id);

    AuthorUpdateDto ToUpdateDto(AuthorDetailsDto dto);
}