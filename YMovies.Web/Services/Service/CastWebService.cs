using System.Collections.Generic;
using AutoMapper;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.Web.DTOs;

namespace YMovies.Web.Services.Service
{
    public class CastWebService
    {
        private readonly IRepository<Cast> _repository;
        public CastWebService(CastRepository repository) => _repository = repository;

        private static readonly MapperConfiguration Config =
            new MapperConfiguration(cfg => cfg.CreateMap<Cast, CastWebDto>());

        private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<CastWebDto> Items => _mapper.Map<IEnumerable<Cast>, IEnumerable<CastWebDto>>(_repository.Items);

        public CastWebDto GetItem(int id)
        {
            var cast = _repository.GetItem(id);
            return _mapper.Map<Cast, CastWebDto>(cast);
        }

        public void AddItem(CastWebDto item)
        {
             var cast = _mapper.Map<CastWebDto, Cast>(item);
             _repository.AddItem(cast);
        }

        public void UpdateItem(CastWebDto item)
        {
             var cast = _mapper.Map<CastWebDto, Cast>(item);
             _repository.UpdateItem(cast);
        }

        public void DeleteItem(CastWebDto item)
        {
             var cast = _mapper.Map<CastWebDto, Cast>(item);
             _repository.DeleteItem(cast.Id);
        }
    }
}