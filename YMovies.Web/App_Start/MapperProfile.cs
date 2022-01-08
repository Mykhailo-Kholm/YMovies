using AutoMapper;
using Ymovies.Identity.BLL.DTO;
using YMovies.MovieDbService.Models;
using YMovies.Web.DTOs;
using YMovies.Web.Models;

namespace YMovies.Web.App_Start
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RegisterViewModel, UserDTO>().
                ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();
            CreateMap<Media, MovieWebDto>().ReverseMap();
            CreateMap<Cast, CastWebDto>().ReverseMap();
            CreateMap<Country, CountryWebDto>().ReverseMap();
            CreateMap<Genre, GenreWebDto>().ReverseMap();
            CreateMap<Season, SeasonWebDto>().ReverseMap();
            CreateMap<Media, SeriesWebDto>().ReverseMap();
        }
    }
}