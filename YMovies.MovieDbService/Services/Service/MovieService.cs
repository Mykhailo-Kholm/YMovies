using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.MovieDbService.Services.Service
{
    public class MovieService : IService<MediaDto>
    {
        private readonly IRepository<Media> _repository;
        public MovieService(MovieRepository repository) => _repository = repository;
        private static readonly MapperConfiguration Config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Type, TypeDto>();
            cfg.CreateMap<Cast, CastDto>();
            cfg.CreateMap<Season, SeasonDto>();
            cfg.CreateMap<Country, CountryDto>();
            cfg.CreateMap<Genre, GenreDto>();
            cfg.CreateMap<Media, MediaDto>();
        });
        //static readonly MapperConfiguration Config = new MapperConfiguration(cfg => cfg.CreateMap<Media, MediaDto>()
        //    .ForMember("Type", opt => opt.MapFrom(m => m.Type.Name)).ReverseMap());

        private readonly Mapper _mapper = new Mapper(Config);

        private List<MediaDto> data;

        public IEnumerable<MediaDto> Items
        {
            get
            {
                if (data == null)
                    data = _mapper.Map<IEnumerable<Media>, IEnumerable<MediaDto>>(_repository.Items).ToList();
                return data;
            }
        }
        
        public MediaDto GetItem(int id)
        {
            var movie = _repository.GetItem(id);
            return _mapper.Map<Media, MediaDto>(movie);
        }

        public void AddItem(MediaDto item)
        {
            var movie = _mapper.Map<MediaDto, Media>(item);
            _repository.AddItem(movie);
        }

        public void UpdateItem(MediaDto item)
        {
            var movie = _mapper.Map<MediaDto, Media>(item);
            _repository.UpdateItem(movie);
        }

        public void DeleteItem(MediaDto item)
        {
            var movie = _mapper.Map<MediaDto, Media>(item);
            _repository.DeleteItem(movie.MediaId);
        }
    }
}
