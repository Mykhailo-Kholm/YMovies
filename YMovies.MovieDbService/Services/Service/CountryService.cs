using AutoMapper;
using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.MovieDbService.Services.Service
{
    public class CountryService:IService<GenresDto>
    {
        private readonly IRepository<Country> _repository;
        public CountryService(CountryRepository repository) => _repository = repository;

        private static readonly MapperConfiguration Config =
            new MapperConfiguration(cfg => cfg.CreateMap<Country, GenresDto>());

        private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<GenresDto> Items => _mapper.Map<IEnumerable<Country>, IEnumerable<GenresDto>>(_repository.Items);

        public GenresDto GetItem(int id)
        {
            var country = _repository.GetItem(id);
            return _mapper.Map<Country, GenresDto>(country);
        }

        public void AddItem(GenresDto item)
        {
            var country = _mapper.Map<GenresDto, Country>(item);
            _repository.AddItem(country);
        }

        public void UpdateItem(GenresDto item)
        {
            var country = _mapper.Map<GenresDto, Country>(item);
            _repository.UpdateItem(country);
        }

        public void DeleteItem(GenresDto item)
        {
            var country = _mapper.Map<GenresDto, Country>(item);
            _repository.DeleteItem(country.Id);
        }
    }
}
