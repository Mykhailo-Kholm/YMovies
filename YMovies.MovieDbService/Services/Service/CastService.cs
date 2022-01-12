using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.Utilities;

namespace YMovies.MovieDbService.Services.Service
{
    public class CastService : IService<CastDto>
    {
        private readonly IRepository<Cast> _repository;
        public CastService(CastRepository repository) => _repository = repository;

        public IEnumerable<CastDto> Items => AutoMap.Mapper.Map<IEnumerable<Cast>, IEnumerable<CastDto>>(_repository.Items);
        public CastDto GetItem(int id)
        {
            var cast = _repository.GetItem(id);
            return AutoMap.Mapper.Map<Cast, CastDto>(cast);
        }

        public void AddItem(CastDto item)
        {
            var cast = AutoMap.Mapper.Map<CastDto, Cast>(item);
            _repository.AddItem(cast);
        }

        public void UpdateItem(CastDto item)
        {
            var cast = AutoMap.Mapper.Map<CastDto, Cast>(item);
            _repository.UpdateItem(cast);
        }

        public void DeleteItem(CastDto item)
        {
            var cast = AutoMap.Mapper.Map<CastDto, Cast>(item);
            _repository.DeleteItem(cast.Id);
        }
    }
}
