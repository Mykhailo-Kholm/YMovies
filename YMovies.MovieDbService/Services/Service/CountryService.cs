using AutoMapper;
using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.MovieDbService.Services.Service
{
    class CountryService:IService<CountryDto>
    {
        private readonly IRepository<Country> _repository;
        public CountryService(CountryRepository repository) => _repository = repository;

        private static readonly MapperConfiguration Config =
            new MapperConfiguration(cfg => cfg.CreateMap<Country, CountryDto>());

        private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<CountryDto> Items => _mapper.Map<IEnumerable<Country>, IEnumerable<CountryDto>>(_repository.Items);

        public CountryDto GetItem(int id)
        {
            var country = _repository.GetItem(id);
            return _mapper.Map<Country, CountryDto>(country);
        }

        public void AddItem(CountryDto item)
        {
            var country = _mapper.Map<CountryDto, Country>(item);
            _repository.AddItem(country);
        }

        public void UpdateItem(CountryDto item)
        {
            var country = _mapper.Map<CountryDto, Country>(item);
            _repository.UpdateItem(country);
        }

        public void DeleteItem(CountryDto item)
        {
            var country = _mapper.Map<CountryDto, Country>(item);
            _repository.DeleteItem(country.Id);
        }
    }
}
