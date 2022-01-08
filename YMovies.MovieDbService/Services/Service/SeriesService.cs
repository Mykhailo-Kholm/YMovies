using AutoMapper;
using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.MovieDbService.Services.Service
{
    public class SeriesService:IService<SeriesDto>
    {
        private readonly IRepository<Series> _repository;
        public SeriesService(SeriesRepository repository) => _repository = repository;

        static readonly MapperConfiguration Config = new MapperConfiguration(cfg => cfg.CreateMap<Series, SeriesDto>()
            .ForMember("Type", opt => opt.MapFrom(m => m.Type.Name)));
        private readonly Mapper _mapper = new Mapper(Config);

        public IEnumerable<SeriesDto> Items => _mapper.Map<IEnumerable<Series>, IEnumerable<SeriesDto>>(_repository.Items);
        public SeriesDto GetItem(int id)
        {
            var series = _repository.GetItem(id);
            return _mapper.Map<Series, SeriesDto>(series);
        }

        public void AddItem(SeriesDto item)
        {
            var series = _mapper.Map<SeriesDto, Series>(item);
            _repository.AddItem(series);
        }

        public void UpdateItem(SeriesDto item)
        {
            var series = _mapper.Map<SeriesDto, Series>(item);
            _repository.UpdateItem(series);
        }

        public void DeleteItem(SeriesDto item)
        {
            var series = _mapper.Map<SeriesDto, Series>(item);
            _repository.DeleteItem(series.SeriesId);
        }
    }
}
