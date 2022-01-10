using YMovies.MovieDbService.Models;

namespace YMovies.MovieDbService.Repositories.IRepository
{
    interface ISearchRepository:IRepository<Media>
    {
        Media GetItem(string id);
        Media GetMostPopular();
        Media GetMediaByTitle(string title);
        Media GetMediaByParams(string genre, string country, string year, string type);
    }
}
