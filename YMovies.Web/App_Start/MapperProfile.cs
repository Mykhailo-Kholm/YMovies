using AutoMapper;
using Ymovies.Identity.BLL.DTO;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.Web.Dtos;
using YMovies.Web.DTOs;
using YMovies.Web.Models;

namespace YMovies.Web.App_Start
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Type, TypeWebDto>().ReverseMap();
            CreateMap<Media, MovieWebDto>().ForMember(prt=>prt.Type, opt => opt.MapFrom(m => m.Type.Name)).ReverseMap();
            CreateMap<RegisterViewModel, UserDTO>().
                ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();
            CreateMap<Cast, CastWebDto>().ReverseMap();
            CreateMap<Country, CountryWebDto>().ReverseMap();
            CreateMap<Genre, GenreWebDto>().ReverseMap();
            CreateMap<Season, SeasonWebDto>().ReverseMap();
            CreateMap<Media, SeriesWebDto>().ReverseMap();

            CreateMap<Cast, CastDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Season, SeasonDto>()
                .ForMember(d => d.CurrentSeries, opt => opt.MapFrom(m => m.CurrentSeries))
                .ReverseMap();
            CreateMap<Type, TypeDto>().ReverseMap();
            CreateMap<Media, MediaDto>()
                .ForMember(d => d.Cast, opt => opt.MapFrom(m => m.Cast))
                .ForMember(d => d.Countries, opt => opt.MapFrom(m => m.Countries))
                .ForMember(d => d.Genres, opt => opt.MapFrom(m => m.Genres))
                .ForMember(d => d.Seasons, opt => opt.MapFrom(m => m.Seasons))
                .ForMember(d => d.Type, opt => opt.MapFrom(m => m.Type.Name))
                .ReverseMap();
        }
    }
}