using AutoMapper;
using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.MovieDbService.Services.Service
{
    public class CastService : IService<CastDto>
    {
        private readonly IRepository<Cast> _repository;
        public CastService(CastRepository repository) => _repository = repository;

        private static readonly MapperConfiguration Config =
            new MapperConfiguration(cfg => cfg.CreateMap<Cast, CastDto>());

        private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<CastDto> Items => _mapper.Map<IEnumerable<Cast>, IEnumerable<CastDto>>(_repository.Items);
        public CastDto GetItem(int id)
        {
            var cast = _repository.GetItem(id);
            return _mapper.Map<Cast, CastDto>(cast);
        }

        public void AddItem(CastDto item)
        {
            var cast = _mapper.Map<CastDto, Cast>(item);
            _repository.AddItem(cast);
        }

        public void UpdateItem(CastDto item)
        {
            var cast = _mapper.Map<CastDto, Cast>(item);
            _repository.UpdateItem(cast);
        }

        public void DeleteItem(CastDto item)
        {
            var cast = _mapper.Map<CastDto, Cast>(item);
            _repository.DeleteItem(cast.Id);
        }
    }
}
