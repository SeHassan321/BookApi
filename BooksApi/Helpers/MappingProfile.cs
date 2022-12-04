using AutoMapper;
using BooksApi.Dto;
using BooksApi.Models;
using BooksApi.Models.Identity;

namespace BooksApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Language, LanguageDto>();
            CreateMap<Nationality, NationalityDto>();
            CreateMap<Auther, AutherDto>();

            CreateMap<Book, BookViewDto>()
                .ForMember(d => d.AutherName, option => option.MapFrom(s => s.Auther.Name))
                .ForMember(d=>d.language,option=>option.MapFrom(s=>s.Auther.Nationalities.Language.Name))
                .ForMember(d => d.Images, option => option.MapFrom(s => s.Images.Select(m => m.Name)));

            CreateMap<ApplicationUser, CurrentUserDTO>()
                .ForMember(d => d.City, o => o.MapFrom(s => s.Addresses.City))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Addresses.Country))
                .ForMember(d => d.Phone, o => o.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.UserName))
                .ForMember(d => d.BookImage, o => o.MapFrom(s => s.FavoritLists.Select(U => U.BookFavourite.Select(b => b.Images.Select(i=>i.Name)))));

            CreateMap<LanguageDto, Language>();
            CreateMap<NationalityDto, Nationality>();
            CreateMap<AutherDto, Auther>();
            CreateMap<BookDto, Book>();
        }
    }
}
