using AutoMapper;
using System.Collections.Generic;
using YMovies.Identity.Users;
using YMovies.Web.Dtos;
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
        }
    }
}