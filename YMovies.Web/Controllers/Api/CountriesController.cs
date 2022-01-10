using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.Web.Controllers.Api
{
    public class CountriesController : ApiController
    {
        public CountriesController(IService<CountryDto> countriesService)
        {
            this._countriesService = countriesService;
        }

        public CountriesController()
        {
        }

        private IService<CountryDto> _countriesService;
       
        public IEnumerable<CountryDto> GetCountries(string query = null)
        {
            var resultList = _countriesService.Items.AsQueryable();
                            
            if (!string.IsNullOrWhiteSpace(query))
                resultList = resultList.Where(r => r.Name.Contains(query));
            
            return resultList.ToList();
        }       
    }
}