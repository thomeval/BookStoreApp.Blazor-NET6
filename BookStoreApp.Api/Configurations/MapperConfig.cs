using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models.Author;
using BookStoreApp.Api.Models.Book;
using BookStoreApp.Api.Models.User;

namespace BookStoreApp.Api.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<AuthorCreateDto, Author>().ReverseMap();
        CreateMap<AuthorGetAllDto, Author>().ReverseMap();
        CreateMap<AuthorGetSingleDto, Author>().ReverseMap();
        CreateMap<AuthorDetailsDto, Author>().ReverseMap();
        CreateMap<AuthorUpdateDto, Author>().ReverseMap();
    

        CreateMap<BookCreateDto, Book>().ReverseMap();
        CreateMap<BookGetAllDto, Book>().ReverseMap().ForMember(m => m.AuthorName, act => act.MapFrom(b => $"{b.Author.FirstName} {b.Author.LastName}"));
        CreateMap<BookGetSingleDto, Book>().ReverseMap().ForMember(m => m.AuthorName, act => act.MapFrom(b => $"{b.Author.FirstName} {b.Author.LastName}"));
        CreateMap<BookUpdateDto, Book>().ReverseMap();

        CreateMap<UserDto, ApiUser>().ReverseMap();
    }
}
