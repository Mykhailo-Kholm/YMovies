using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using YMovies.Database.DTOs;
using YMovies.Database.Repositories.Repository;
using YMovies.Database.Services.IService;

namespace YMovies.Database.Services.Service
{
    class MovieService:IService<MovieDto>
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
