using AutoMapper;

namespace YMovies.MovieDbService.Utilities
{
    public class AutoMap
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
