using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services;

public interface IAuthorService
{
    Task<Response<AuthorDetailsDto>> Get(int id);
    Task<Response<List<AuthorGetAllDto>>> GetAll();
    Task<Response<int>> Create(AuthorCreateDto author);
    Task<Response<int>> Update(int id, AuthorUpdateDto author);
    Task<Response<int>> Delete(int id);

    AuthorUpdateDto ToUpdateDto(AuthorDetailsDto dto);

}