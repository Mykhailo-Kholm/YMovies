using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMovies.Web.IMDB.DBWorker
{
    interface ISeed
    {
        Task AddMovieByImbdId(string imdbId);
        Task AddMediaByExpression(string expression);
    }
}
