using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models.Author;

namespace BookStoreApp.Api.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<AuthorCreateDto, Author>().ReverseMap();
        CreateMap<AuthorGetAllDto, Author>().ReverseMap();
        CreateMap<AuthorGetSingleDto, Author>().ReverseMap();
        CreateMap<AuthorUpdateDto, Author>().ReverseMap();
    }
}
