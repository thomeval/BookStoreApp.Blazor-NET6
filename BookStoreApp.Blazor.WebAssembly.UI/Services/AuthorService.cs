using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.WebAssembly.UI.Models;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services;

public class AuthorService : BaseHttpService, IAuthorService
{
    private readonly IMapper _mapper;

    public AuthorService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
    {
        _mapper = mapper;
    }

    public async Task<Response<AuthorGetAllDtoVirtualizeResponse>> GetAll(QueryParameters queryParams)
    {
        try
        {
            await GetBearerToken();
            var data = await Client.AuthorsGETAsync(queryParams.StartIndex, queryParams.PageSize);

            var response = new Response<AuthorGetAllDtoVirtualizeResponse>
            {
                Data = data,
                Success = true
            };
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<AuthorGetAllDtoVirtualizeResponse>(ex);
        }
    }

    public async Task<Response<List<AuthorGetAllDto>>> GetAll()
    {
        try
        {
            await GetBearerToken();
            var data = await Client.GetAllAsync();

            var response = new Response<List<AuthorGetAllDto>>
            {
                Data = data.ToList(),
                Success = true
            };
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<List<AuthorGetAllDto>>(ex);
        }
    }

    public async Task<Response<int>> Create(AuthorCreateDto author)
    {
        try
        {
            await GetBearerToken();
            await Client.AuthorsPOSTAsync(author);

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

    public async Task<Response<int>> Update(int id, AuthorUpdateDto author)
    {
        try
        {
            await GetBearerToken();
            await Client.AuthorsPUTAsync(id, author);

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
            await Client.AuthorsDELETEAsync(id);

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

    public async Task<Response<AuthorDetailsDto>> Get(int id)
    {
        try
        {
            await GetBearerToken();
            var data = await Client.AuthorsGET2Async(id);

            
            var response = new Response<AuthorDetailsDto>
            {
                Success = true,
                Data = data,
            };
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<AuthorDetailsDto>(ex);
        }
    }

    public AuthorUpdateDto ToUpdateDto(AuthorDetailsDto dto)
    {
        return _mapper.Map<AuthorUpdateDto>(dto);
    }

}