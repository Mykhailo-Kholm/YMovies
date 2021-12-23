using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using IMDbApiLib;

namespace YMovies.Web.IMDB
{
    public class AMDBTest
    {
        public async Task TestMethod()
        {
            var apiLib = new ApiLib("k_12345678");
            var data = await apiLib.TitleAsync("tt0110413");
        }
    }
}