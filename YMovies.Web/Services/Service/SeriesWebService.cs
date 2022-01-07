using AutoMapper;
using System.Collections.Generic;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.Web.DTOs;
using YMovies.Web.Utilities;

namespace YMovies.Web.Services.Service
{
    public class SeriesWebService
    {
        private readonly IRepository<Series> _repository;
        public SeriesWebService(SeriesRepository repository) => _repository = repository;

        //static readonly MapperConfiguration Config = new MapperConfiguration(cfg => cfg.CreateMap<Series, SeriesWebDto>()
        //    .ForMember("Type", opt => opt.MapFrom(m => m.Type.Name)));
        //private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<SeriesWebDto> Items => AutoMap.Mapper.Map<IEnumerable<Series>, IEnumerable<SeriesWebDto>>(_repository.Items);

        public SeriesWebDto GetItem(int id)
        {
            var series = _repository.GetItem(id);
            return AutoMap.Mapper.Map<Series, SeriesWebDto>(series);
        }

        public void AddItem(SeriesWebDto item)
        {
            var series = AutoMap.Mapper.Map<SeriesWebDto, Series>(item);
            _repository.AddItem(series);
        }

        public void UpdateItem(SeriesWebDto item)
        {
            var series = AutoMap.Mapper.Map<SeriesWebDto, Series>(item);
            _repository.UpdateItem(series);
        }

        public void DeleteItem(SeriesWebDto item)
        {
            var series = AutoMap.Mapper.Map<SeriesWebDto, Series>(item);
            _repository.DeleteItem(series.SeriesId);
        }
    }
}