using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMDbApiLib;

namespace YMovies.Web.IMDB
{
    public class APIworker
    {
        public async void GetTop250Movies()
        {
            var apiLib = new ApiLib("k_12345678");
            var data = await apiLib.MostPopularMoviesAsync();
        }
    }
}