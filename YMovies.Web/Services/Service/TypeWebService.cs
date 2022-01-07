using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.Web.Dtos;
using YMovies.Web.DTOs;
using YMovies.Web.Utilities;
using Type = YMovies.MovieDbService.Models.Type;

namespace YMovies.Web.Services.Service
{
    public class TypeWebService
    {
        private readonly IRepository<Type> _repository;
        public TypeWebService(TypeRepository repository) => _repository = repository;

        //static readonly MapperConfiguration Config = new MapperConfiguration(cfg => cfg.CreateMap<Series, SeriesWebDto>()
        //    .ForMember("Type", opt => opt.MapFrom(m => m.Type.Name)));
        //private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<TypeWebDto> Items => AutoMap.Mapper.Map<IEnumerable<Type>, IEnumerable<TypeWebDto>>(_repository.Items);

        public TypeWebDto GetItem(int id)
        {
            var series = _repository.GetItem(id);
            return AutoMap.Mapper.Map<Type, TypeWebDto>(series);
        }

        public void AddItem(TypeWebDto item)
        {
            var series = AutoMap.Mapper.Map<TypeWebDto, Type>(item);
            _repository.AddItem(series);
        }

        public void UpdateItem(TypeWebDto item)
        {
            var series = AutoMap.Mapper.Map<TypeWebDto, Type>(item);
            _repository.UpdateItem(series);
        }

        public void DeleteItem(TypeWebDto item)
        {
            var series = AutoMap.Mapper.Map<TypeWebDto, Type>(item);
            _repository.DeleteItem(series.Id);
        }
    }
}