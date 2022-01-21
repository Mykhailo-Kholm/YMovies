using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.Utilities;

namespace YMovies.MovieDbService.Services.Service
{
    public class GenreService:IService<GenreDto>
    {
        private readonly IRepository<Genre> _repository;
        public GenreService(GenreRepository repository) => _repository = repository;

        public IEnumerable<GenreDto> Items => AutoMap.Mapper.Map<IEnumerable<Genre>, List<GenreDto>>(_repository.Items);

        public GenreDto GetItem(int id)
        {
            var genre = _repository.GetItem(id);
            return AutoMap.Mapper.Map<Genre, GenreDto>(genre);
        }

        public void AddItem(GenreDto item)
        {
            var genre = AutoMap.Mapper.Map<GenreDto, Genre>(item);
            _repository.AddItem(genre);
        }

        public void UpdateItem(GenreDto item)
        {
            var genre = AutoMap.Mapper.Map<GenreDto, Genre>(item);
            _repository.UpdateItem(genre);
        }

        public void DeleteItem(GenreDto item)
        {
            var genre = AutoMap.Mapper.Map<GenreDto, Genre>(item);
            _repository.DeleteItem(genre.Id);
        }
    }
}
