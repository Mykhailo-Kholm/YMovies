using AutoMapper;
using System.Collections.Generic;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.Web.DTOs;

namespace YMovies.Web.Services.Service
{
    public class CountryWebService
    {
        private readonly IRepository<Country> _repository;
        public CountryWebService(CountryRepository repository) => _repository = repository;

        private static readonly MapperConfiguration Config =
            new MapperConfiguration(cfg => cfg.CreateMap<Country, CountryWebDto>());

        private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<CountryWebDto> Items => _mapper.Map<IEnumerable<Country>, IEnumerable<CountryWebDto>>(_repository.Items);

        public CountryWebDto GetItem(int id)
        {
            var country = _repository.GetItem(id);
            return _mapper.Map<Country, CountryWebDto>(country);
        }

        public void AddItem(CountryWebDto item)
        {
            var country = _mapper.Map<CountryWebDto, Country>(item);
            _repository.AddItem(country);
        }

        public void UpdateItem(CountryWebDto item)
        {
            var country = _mapper.Map<CountryWebDto, Country>(item);
            _repository.UpdateItem(country);
        }

        public void DeleteItem(CountryWebDto item)
        {
            var country = _mapper.Map<CountryWebDto, Country>(item);
            _repository.DeleteItem(country.Id);
        }
    }
}