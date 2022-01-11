using AutoMapper;
using Ymovies.Identity.BLL.DTO;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;

namespace YMovies.MovieDbService.Utilities
{
    class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDTO, UserDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(m => m.Id))
                .ForMember(d => d.FullName, opt => opt.MapFrom(m => m.Name + m.SecondName))
                .ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Cast, CastDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Season, SeasonDto>()
                .ForMember(d => d.CurrentSeries, opt => opt.MapFrom(m => m.CurrentSeries))
                .ReverseMap();
            CreateMap<Models.Type, TypeDto>().ReverseMap();
            CreateMap<Media, MediaDto>()
                .ForMember(d => d.Cast, opt => opt.MapFrom(m => m.Cast))
                .ForMember(d => d.Countries, opt => opt.MapFrom(m => m.Countries))
                .ForMember(d => d.Genres, opt => opt.MapFrom(m => m.Genres))
                .ForMember(d => d.Seasons, opt => opt.MapFrom(m => m.Seasons))
                .ForMember(d => d.Type, opt => opt.MapFrom(m => m.Type))
                .ReverseMap();                        
        }
    }
}
