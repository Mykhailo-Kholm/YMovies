using AutoMapper;
using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.Utilities;

namespace YMovies.MovieDbService.Services.Service
{
    public class CountryService:IService<CountryDto>
    {
        private readonly IRepository<Country> _repository;
        public CountryService(CountryRepository repository) => _repository = repository;

        //private static readonly MapperConfiguration Config =
        //    new MapperConfiguration(cfg => cfg.CreateMap<Country, CountryDto>().ReverseMap());

        //private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<CountryDto> Items => AutoMap.Mapper.Map<IEnumerable<Country>, IEnumerable<CountryDto>>(_repository.Items);

        public CountryDto GetItem(int id)
        {
            var country = _repository.GetItem(id);
            return AutoMap.Mapper.Map<Country, CountryDto>(country);
        }

        public void AddItem(CountryDto item)
        {
            var country = AutoMap.Mapper.Map<CountryDto, Country>(item);
            _repository.AddItem(country);
        }

        public void UpdateItem(CountryDto item)
        {
            var country = AutoMap.Mapper.Map<CountryDto, Country>(item);
            _repository.UpdateItem(country);
        }

        public void DeleteItem(CountryDto item)
        {
            var country = AutoMap.Mapper.Map<CountryDto, Country>(item);
            _repository.DeleteItem(country.Id);
        }
    }
}
