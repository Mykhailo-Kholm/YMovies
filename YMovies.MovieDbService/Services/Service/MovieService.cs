using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.Utilities;

namespace YMovies.MovieDbService.Services.Service
{
    public class MovieService : IService<MediaDto>
    {
        private readonly IRepository<Media> _repository;
        public MovieService(MovieRepository repository) => _repository = repository;
        
        private IEnumerable<MediaDto> data;

        public IEnumerable<MediaDto> Items => data ?? (data = AutoMap.Mapper.Map<IEnumerable<Media>, List<MediaDto>>(_repository.Items.ToList()));

        public MediaDto GetItem(int id)
        {
            var movie = _repository.GetItem(id);
            return AutoMap.Mapper.Map<Media, MediaDto>(movie);
        }

        public void AddItem(MediaDto item)
        {
            var movie = AutoMap.Mapper.Map<MediaDto, Media>(item);
            _repository.AddItem(movie);
        }

        public void UpdateItem(MediaDto item)
        {
            var movie = AutoMap.Mapper.Map<MediaDto, Media>(item);
            _repository.UpdateItem(movie);
        }

        public void DeleteItem(MediaDto item)
        {
            var movie = AutoMap.Mapper.Map<MediaDto, Media>(item);
            _repository.DeleteItem(movie.MediaId);
        }
    }
}
