﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.Web.DTOs;
using YMovies.Web.Utilities;

namespace YMovies.Web.Services.Service
{
    public class MovieWebService
    {
        private readonly IRepository<Media> _repository;
        public MovieWebService(MovieRepository repository) => _repository = repository;
        //static readonly MapperConfiguration Config = new MapperConfiguration(cfg => cfg.CreateMap<Media, MovieWebDto>()
        //    .ForMember("Type", opt => opt.MapFrom(m => m.Type.Name)));
        //private readonly Mapper _mapper = new Mapper(Config);
        public IEnumerable<MovieWebDto> Items => AutoMap.Mapper.Map<IEnumerable<Media>, IEnumerable<MovieWebDto>>(_repository.Items);
        public MovieWebDto GetItem(int id)
        {
            var movie = _repository.GetItem(id);
            return AutoMap.Mapper.Map<Media, MovieWebDto>(movie);
        }

        public void AddItem(MovieWebDto item)
        {
            var movie = AutoMap.Mapper.Map<MovieWebDto, Media>(item);
            _repository.AddItem(movie);
        }

        public void UpdateItem(MovieWebDto item)
        {
            var movie = AutoMap.Mapper.Map<MovieWebDto, Media>(item);
            _repository.UpdateItem(movie);
        }

        public void DeleteItem(MovieWebDto item)
        {
            var movie = AutoMap.Mapper.Map<MovieWebDto, Media>(item);
            _repository.DeleteItem(movie.MediaId);
        }
    }
}