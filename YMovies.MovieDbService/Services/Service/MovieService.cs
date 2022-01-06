using AutoMapper;
using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.MovieDbService.Services.Service
{
    public class MovieService:IService<MovieDto>
    {
        private readonly IRepository<Movie> _repository;
        public MovieService(MovieRepository repository) => _repository = repository;

        static readonly MapperConfiguration Config = new MapperConfiguration(cfg => cfg.CreateMap<Movie, MovieDto>()
            .ForMember("Type", opt => opt.MapFrom(m => m.Type.Name)));
        private readonly Mapper _mapper = new Mapper(Config);

        public IEnumerable<MovieDto> Items => _mapper.Map<IEnumerable<Movie>, IEnumerable<MovieDto>>(_repository.Items);
        public MovieDto GetItem(int id)
        {
            var movie = _repository.GetItem(id);
            return _mapper.Map<Movie, MovieDto>(movie);
        }

        public void AddItem(MovieDto item)
        {
            var movie = _mapper.Map<MovieDto, Movie>(item);
            _repository.AddItem(movie);
        }

        public void UpdateItem(MovieDto item)
        {
            var movie = _mapper.Map<MovieDto, Movie>(item);
            _repository.UpdateItem(movie);
        }

        public void DeleteItem(MovieDto item)
        {
            var movie = _mapper.Map<MovieDto, Movie>(item);
            _repository.DeleteItem(movie.MovieId);
        }
    }
}
