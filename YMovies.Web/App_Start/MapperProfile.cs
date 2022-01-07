using AutoMapper;
using YMovies.Identity.Models;
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
            CreateMap<ApplicationUser, RegisterViewModel>().ReverseMap();
            CreateMap<ApplicationUser, IndexViewModel>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<Movie, MovieWebDto>().ReverseMap();
            CreateMap<Cast, CastWebDto>().ReverseMap();
            CreateMap<Country, CountryWebDto>().ReverseMap();
            CreateMap<Genre, GenreWebDto>().ReverseMap();
            CreateMap<Season, SeasonWebDto>().ReverseMap();
            CreateMap<Series, SeriesWebDto>().ReverseMap();
        }
    }
}