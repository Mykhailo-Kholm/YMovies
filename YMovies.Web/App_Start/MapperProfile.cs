using AutoMapper;
using IMDbApiLib.Models;
using Ymovies.Identity.BLL.DTO;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.Web.DTOs;
using YMovies.Web.Models;
using YMovies.Web.Models.AdminViewModels;
using YMovies.Web.Models.MoviesInfoViewModel;
using YMovies.Web.Utilites;
using YMovies.Web.ViewModels;

namespace YMovies.Web.App_Start
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDTO, ManageUserRightsViewModel>().
                ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name + " " + src.SecondName));
            CreateMap<UserDTO, FindUserViewModel>()
                .ReverseMap();
            CreateMap<MediaDto, IndexMediaViewModel>()
                .ReverseMap();
            CreateMap<MostPopularDataDetail, IndexMediaViewModel>()
                 .ForMember(dest => dest.ImdbRating,
                     opt => opt.MapFrom(src => TypeConverter.ToDecimal(src.IMDbRating)));
            CreateMap<Top250DataDetail, IndexMediaViewModel>()
                .ForMember(dest => dest.ImdbRating,
                    opt => opt.MapFrom(src => TypeConverter.ToDecimal(src.IMDbRating)));
            CreateMap<Top250DataDetail, MediaDto>()
               .ForMember(dest => dest.ImdbRating,
                   opt => opt.MapFrom(src => TypeConverter.ToDecimal(src.IMDbRating)));
            CreateMap<IndexMediaViewModel, Top250DataDetail>()
                .ReverseMap();
            CreateMap<Media, MovieWebDto>()
                .ForMember(prt => prt.Type, opt => opt.MapFrom(m => m.Type.Name))
                .ReverseMap();
            CreateMap<RegisterViewModel, UserDTO>().
                ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();
            CreateMap<CastDto, CastViewModel>().ReverseMap();
            CreateMap<NewFilmViewModel, MediaDto>()
                .ForMember(dest => dest.NumberOfDislikes, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.NumberOfLikes, opt => opt.Ignore())
                .ForMember(dest => dest.Seasons, opt => opt.Ignore())
                .ForMember(dest => dest.Cast, opt => opt.Ignore())
                .ForMember(dest => dest.UsersLiked, opt => opt.Ignore())
                .ForMember(dest => dest.UsersWatched, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => src.Country));            
        }
    }
}
