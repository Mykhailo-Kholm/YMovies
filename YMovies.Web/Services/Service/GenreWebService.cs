using AutoMapper;
using System.Collections.Generic;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.Web.DTOs;
using YMovies.Web.Utilities;

namespace YMovies.Web.Services.Service
{
    public class GenreWebService
    {
        private readonly IRepository<Genre> _repository;
        public GenreWebService(GenreRepository repository) => _repository = repository;

        //private static readonly MapperConfiguration Config =
        //    new MapperConfiguration(cfg => cfg.CreateMap<Genre, GenreWebDto>());

        //private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<GenreWebDto> Items => AutoMap.Mapper.Map<IEnumerable<Genre>, IEnumerable<GenreWebDto>>(_repository.Items);
        public GenreWebDto GetItem(int id)
        {
            var genre = _repository.GetItem(id);
            return AutoMap.Mapper.Map<Genre, GenreWebDto>(genre);
        }

        public void AddItem(GenreWebDto item)
        {
            var genre = AutoMap.Mapper.Map<GenreWebDto, Genre>(item);
            _repository.AddItem(genre);
        }

        public void UpdateItem(GenreWebDto item)
        {
            var genre = AutoMap.Mapper.Map<GenreWebDto, Genre>(item);
            _repository.UpdateItem(genre);
        }

        public void DeleteItem(GenreWebDto item)
        {
            var genre = AutoMap.Mapper.Map<GenreWebDto, Genre>(item);
            _repository.DeleteItem(genre.Id);
        }
    }
}