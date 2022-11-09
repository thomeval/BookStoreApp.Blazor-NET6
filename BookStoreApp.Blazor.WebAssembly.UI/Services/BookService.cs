using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services;

public class BookService : BaseHttpService, IBookService
{
    private readonly IMapper _mapper;
    private string _coverImageFolder = "bookcoverimages";

    public BookService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
    {
        _mapper = mapper;
    }

    public async Task<Response<List<BookGetAllDto>>> GetAll()
    {
        try
        {
            await GetBearerToken();
            var books = await Client.BooksAllAsync();

            var response = new Response<List<BookGetAllDto>>
            {
                Data = books.ToList(),
                Success = true
            };
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<List<BookGetAllDto>>(ex);
        }
    }

    public async Task<Response<int>> Create(BookCreateDto book)
    {
        try
        {
            await GetBearerToken();
            await Client.BooksPOSTAsync(book);

            var response = new Response<int>
            {
                Success = true
            };
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<int>(ex);
        }
    }

    public async Task<Response<int>> Update(int id, BookUpdateDto book)
    {
        try
        {
            await GetBearerToken();
            await Client.BooksPUTAsync(id, book);

            var response = new Response<int>
            {
                Success = true
            };
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<int>(ex);
        }
    }

    public async Task<Response<int>> Delete(int id)
    {
        try
        {
            await GetBearerToken();
            await Client.BooksDELETEAsync(id);

            var response = new Response<int>
            {
                Success = true
            };
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<int>(ex);
        }
    }

    public async Task<Response<BookGetSingleDto>> Get(int id)
    {
        try
        {
            await GetBearerToken();
            var data = await Client.BooksGETAsync(id);

            
            var response = new Response<BookGetSingleDto>
            {
                Success = true,
                Data = data,
            };
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<BookGetSingleDto>(ex);
        }
    }

    public BookUpdateDto ToUpdateDto(BookGetSingleDto dto)
    {
        return _mapper.Map<BookUpdateDto>(dto);
    }

    public string? GetCoverImagePath(string? fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return null;
        }

        return $"{this.BaseUrl}/{_coverImageFolder}/{fileName}";
    }
}