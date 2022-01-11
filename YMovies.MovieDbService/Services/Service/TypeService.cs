using System;
using System.Collections.Generic;
using AutoMapper;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.Utilities;
using Type = YMovies.MovieDbService.Models.Type;

namespace YMovies.MovieDbService.Services.Service
{
    public class TypeService:IService<TypeDto>
    {
        private readonly IRepository<Type> _repository;
        public TypeService(TypeRepository repository) => _repository = repository;
        //private static  readonly MapperConfiguration Config =
        //    new MapperConfiguration(cfg => cfg.CreateMap<Type, TypeDto>());

        //private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<TypeDto> Items => AutoMap.Mapper.Map<IEnumerable<Type>, IEnumerable<TypeDto>>(_repository.Items);
        public TypeDto GetItem(int id)
        {
            var type = _repository.GetItem(id);
            return AutoMap.Mapper.Map<Type, TypeDto>(type);
        }

        public void AddItem(TypeDto item)
        {
            var type = AutoMap.Mapper.Map<TypeDto, Type>(item);
            _repository.AddItem(type);
        }

        public void UpdateItem(TypeDto item)
        {
            var type = AutoMap.Mapper.Map<TypeDto, Type>(item);
            _repository.UpdateItem(type);
        }

        public void DeleteItem(TypeDto item)
        {
            var type = AutoMap.Mapper.Map<TypeDto, Type>(item);
            _repository.DeleteItem(type.Id);
        }
    }
}
