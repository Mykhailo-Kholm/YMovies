using AutoMapper;
using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.Utilities;

namespace YMovies.MovieDbService.Services.Service
{
    public class MovieService:IService<MediaDto>
    {
        private readonly IRepository<Media> _repository;
        public MovieService(MovieRepository repository) => _repository = repository;

        //static readonly MapperConfiguration Config = new MapperConfiguration(cfg => cfg.CreateMap<Media, MediaDto>()
        //    .ForMember("Type", opt => opt.MapFrom(m => m.Type.Name)));
        //private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<MediaDto> Items => AutoMap.Mapper.Map<IEnumerable<Media>, IEnumerable<MediaDto>>(_repository.Items);
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
