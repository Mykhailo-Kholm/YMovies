using System.Collections.Generic;
using YMovies.MovieDbService.Models;

namespace YMovies.MovieDbService.Repositories.IRepository
{
    public interface ISearchRepository : IRepository<Media>
    {
        Media GetItem(string id);
        List<Media> GetOneHundredMediaRandom();
        List<Media> GetMostLiked();
        List<Media> GetMediaByTitle(string title);
        List<Media> GetMediaByParams(string genre = null, string country = null, string year = null, string type = null);
    }
}
