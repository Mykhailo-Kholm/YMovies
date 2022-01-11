using AutoMapper;
using Ymovies.Identity.BLL.DTO;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.Web.Dtos;
using YMovies.Web.DTOs;
using YMovies.Web.Models;
using YMovies.Web.Models.AdminViewModels;
using YMovies.Web.Models.MoviesInfoViewModel;
using YMovies.Web.ViewModels;

namespace YMovies.Web.App_Start
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            MovieDbService.Utilities.AutoMap.RegisterMapping();
            CreateMap<Type, TypeWebDto>().ReverseMap();
            CreateMap<Media, MovieWebDto>()
                .ForMember(prt=>prt.Type, opt => opt.MapFrom(m => m.Type.Name))
                .ReverseMap();
            CreateMap<RegisterViewModel, UserDTO>().
                ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();
            CreateMap<CastDto, CastViewModel>().
                ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name + " "));
            CreateMap<NewFilmViewModel, MediaDto>()
                .ForMember(dest => dest.Cast, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => src.Country));
            CreateMap<UserDTO, ManageUserRightsViewModel>().
                ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name + " " + src.SecondName));
            CreateMap<MediaDto, IndexMediaViewModel>()
                .ReverseMap();
            CreateMap<Cast, CastWebDto>().ReverseMap();
            CreateMap<Country, CountryWebDto>().ReverseMap();
            CreateMap<Genre, GenreWebDto>().ReverseMap();
            CreateMap<Season, SeasonWebDto>().ReverseMap();
        }
    }
}