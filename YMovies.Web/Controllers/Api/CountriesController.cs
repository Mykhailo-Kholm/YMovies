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
        private IService<CountryDto> _countriesService;
        public CountriesController()
        {
        }

        public CountriesController(IService<CountryDto> countriesService)
        {
            this._countriesService = countriesService;
        }

        IEnumerable<CountryDto> tempData = new List<CountryDto>
        {
            new CountryDto
            {
                Id = 1,
                Name = "Ukraine"
            },
            new CountryDto
            {
                Id = 2,
                Name = "Poland"
            },
            new CountryDto
            {
                Id = 3,
                Name = "United Kindom"
            },
        };

        public IEnumerable<CountryDto> GetCountries(string query = null)
        {
            //var resultList = _countriesService.Items.AsQueryable();
            var resultList = tempData;
                            
            if (!string.IsNullOrWhiteSpace(query))
                resultList = resultList.Where(r => r.Name.Contains(query));
            
            return resultList.ToList();
        }       
    }
}