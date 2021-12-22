using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using IMDbApiLib;
using IMDbApiLib.Models;

namespace YMovies.Web.IMDB
{
    public class APIworkerIMDB
    {
        static private string apiKey = "k_ycaf020q";
        public async Task GetTop250MoviesAsync()
        {
            var apiLib = new ApiLib(apiKey);
            var data = await apiLib.Top250MoviesAsync();
            if (data.ErrorMessage is null)
            {
                var list = data.Items;
            }

        }
        /// <summary>
        /// Возвращает фильмы по заданому жанру. Также может влючать временные ограничения.
        /// </summary>
        /// <param name="genre">Пример: AdvancedSearchGenre.Action</param>
        /// <param name="releaseFrom">Дата начала поиска Пример: "2001-01-01"</param>
        /// <param name="releaseTo">Дата окончания поиска Пример: "2011-11-11"</param>
        /// <returns></returns>
        public async Task<List<AdvancedSearchResult>> Get100ByGenre(AdvancedSearchGenre genre = AdvancedSearchGenre.Action, string releaseFrom = null, string releaseTo = null)
        {
            var apiLib = new ApiLib(apiKey);
            var data = await apiLib.AdvancedSearchAsync(new AdvancedSearchInput(){Genres = genre, Count = AdvancedSearchCount.Hundred, ReleaseDateFrom = releaseFrom, ReleaseDateTo = releaseTo });
            return data.Results;
        }

        public async Task<List<AdvancedSearchResult>> Get100FromTop1000Async(AdvancedSearchSort sort = AdvancedSearchSort.Popularity_Ascending)
        {
            var apiLib = new ApiLib(apiKey);
            var data = await apiLib.AdvancedSearchAsync(new AdvancedSearchInput() {TitleGroups = AdvancedSearchTitleGroup.Top_1000, Count = AdvancedSearchCount.Hundred ,Sort = sort});
            return data.Results;
        }

        public async Task<List<AdvancedSearchResult>> Get100FromBottom1000Async()
        {
            var apiLib = new ApiLib(apiKey);
            var data = await apiLib.AdvancedSearchAsync(new AdvancedSearchInput() { TitleGroups = AdvancedSearchTitleGroup.Bottom_1000 , Count = AdvancedSearchCount.Hundred });
            return data.Results;
        }

        public async Task<List<AdvancedSearchResult>> Get100ByCertificate(AdvancedSearchUSCertificate certificate)
        {
            var apiLib = new ApiLib(apiKey);
            var data = await apiLib.AdvancedSearchAsync(new AdvancedSearchInput() {Count = AdvancedSearchCount.Hundred, USCertificates = certificate});
            return data.Results;
        }
        /// <summary>
        /// Метод возвращает 100 фильмов с похожим именем и датой начиная с releaseFrom и заканчивая releaseTo.
        /// </summary>
        /// <param name="title">Тайтл</param>
        /// <param name="releaseFrom">Дата начала поиска Пример: "2001-01-01"</param>
        /// <param name="releaseTo">Пример:Дата окончания поиска Пример: "2011-11-11"</param>
        public async Task<List<AdvancedSearchResult>> Get100ByTitle(string title, string releaseFrom = null, string releaseTo = null)
        {
            var apiLib = new ApiLib(apiKey);
            var data = await apiLib.AdvancedSearchAsync(new AdvancedSearchInput()
                {Count = AdvancedSearchCount.Hundred, Title = title, ReleaseDateFrom = releaseFrom,ReleaseDateTo = releaseTo});
            return data.Results;
        }

        public async Task<List<SearchResult>> SearchFor(string title)
        {
            var apiLib = new ApiLib(apiKey);
            var data = await apiLib.SearchAsync(title);
            return data.Results;
        }

        public async Task<byte[]> ReportForMovie(string id = "tt0110413")
        {
            var apiLib = new ApiLib(apiKey);
            var data = await apiLib.ReportBytesAsync(id,language: Language.en,false);
            return data;
        }

        public async Task<string> MovieBudget(string id = "tt0110413")
        {
            var apiLib = new ApiLib(apiKey);
            var data = await apiLib.TitleAsync(id);
            return data.BoxOffice.Budget;
        }
    }
}