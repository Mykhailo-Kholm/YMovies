using System;
using System.Collections.Generic;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Repositories.Repository;

namespace YMovies.MovieDbService.Services.Service
{
    public class MovieService : IService<MovieDto>
    {
        private readonly MovieRepository _repository;
        public MovieService(MovieRepository repository) => _repository = repository;
        public IEnumerable<MovieDto> Items { get; }
        public MovieDto GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public void AddItem(MovieDto item)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(MovieDto item)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(MovieDto item)
        {
            throw new NotImplementedException();
        }
    }
}
