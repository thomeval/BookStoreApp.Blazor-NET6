using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services;

public interface IBookService
{
    Task<Response<BookGetSingleDto>> Get(int id);
    Task<Response<List<BookGetAllDto>>> GetAll();
    Task<Response<int>> Create(BookCreateDto book);
    Task<Response<int>> Update(int id, BookUpdateDto book);
    Task<Response<int>> Delete(int id);

    BookUpdateDto ToUpdateDto(BookGetSingleDto dto);
    string? GetCoverImagePath(string? filePath);

}