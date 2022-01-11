using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMovies.Web.App_Start;

namespace YMovies.Web.Utilities
{
    public class AutoMapperWeb
    {
        public static IMapper Mapper { get; private set; }

        public static void RegisterMapping()
        {
            var mapperConfiguration = new MapperConfiguration(c =>
                c.AddProfile<MapperProfile>()
            );
            Mapper = new Mapper(mapperConfiguration);
        }
    }
}