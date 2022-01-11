using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YMovies.MovieDbService.DTOs;

namespace YMovies.MovieDbService.Services.IService
{
    public interface ISearchService
    {
        MediaDto GetItem(string id);
        List<MediaDto> GetOneHundredMediaRandom();
        List<MediaDto> GetMostLiked();
        List<MediaDto> GetMediaByTitle(string title);
        List<MediaDto> GetMediaByParams(string[] genre, string[] country, string year, string type);
    }
}
