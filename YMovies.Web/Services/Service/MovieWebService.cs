using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.Web.Dtos;
using YMovies.Web.DTOs;
using YMovies.Web.Utilities;

namespace YMovies.Web.Services.Service
{
    public class MovieWebService
    {
        private readonly IRepository<Media> _repository;
        public MovieWebService(MovieRepository repository) => _repository = repository;

        private static readonly MapperConfiguration Config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Type, TypeWebDto>();
            cfg.CreateMap<Cast, CastWebDto>();
            cfg.CreateMap<Country, CountryWebDto>();
            cfg.CreateMap<Genre, GenreWebDto>();
            cfg.CreateMap<Media, MediaWebDto>();
        });
        private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<MediaWebDto> Items => _mapper.Map<IEnumerable<Media>, IEnumerable<MediaWebDto>>(_repository.Items);
        public MediaWebDto GetItem(int id)
        {
            var movie = _repository.GetItem(id);
            return AutoMap.Mapper.Map<Media, MediaWebDto>(movie);
        }

        public void AddItem(MediaWebDto item)
        {
            var movie = AutoMap.Mapper.Map<MediaWebDto, Media>(item);
            _repository.AddItem(movie);
        }

        public void UpdateItem(MediaWebDto item)
        {
            var movie = AutoMap.Mapper.Map<MediaWebDto, Media>(item);
            _repository.UpdateItem(movie);
        }

        public void DeleteItem(MediaWebDto item)
        {
            var movie = AutoMap.Mapper.Map<MediaWebDto, Media>(item);
            _repository.DeleteItem(movie.MediaId);
        }
    }
}