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
        private ApiLib apiLib;
        private string apiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["IMDBApiKey"];
        public APIworkerIMDB()
        {
            apiLib = new ApiLib(apiKey);
        }
        /// <summary>
        /// топ 250 фильмов IMDB
        /// </summary>
        /// <returns> Task&lt;List&lt;Top250DataDetail&gt;&gt;</returns>
        public async Task<List<Top250DataDetail>> GetTop250MoviesAsync()
        {
            var data = await apiLib.Top250MoviesAsync();
            return data.Items;
        }
        /// <summary>
        /// список фильмов по заданому expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>Task&lt;List&lt;SearchResult&gt;&gt;</returns>
        public async Task<List<SearchResult>> SearchMovieAsync(string expression)
        {
            var data = await apiLib.SearchMovieAsync(expression);
            return data.Results;
        }
        /// <summary>
        /// список Сериалов по заданому expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>Task&lt;List&lt;SearchResult&gt;&gt;</returns>
        public async Task<List<SearchResult>> SearchSeriesAsync(string expression)
        {
            var data = await apiLib.SearchSeriesAsync(expression);
            return data.Results;
        }
        /// <summary>
        /// Возвращает до 100 фильмов по умолчанию или по заданым параметрам(title, genre, releaseFrom, releaseTo, certificate, sort, group)
        /// </summary>
        /// <param name="title"></param>
        /// <param name="genre"></param>
        /// <param name="releaseFrom"></param>
        /// <param name="releaseTo"></param>
        /// <param name="certificate"></param>
        /// <param name="sort"></param>
        /// <param name="group"></param>
        /// <returns>Task&lt;List&lt;AdvancedSearchResult&gt;&gt;</returns>
        public async Task<List<AdvancedSearchResult>> GetOneHundredFilmsAsync(string title = null,
            AdvancedSearchGenre genre = 0, 
            string releaseFrom = null, 
            string releaseTo = null,
            AdvancedSearchUSCertificate certificate = 0,
            AdvancedSearchSort sort = AdvancedSearchSort.Popularity_Ascending,
            AdvancedSearchTitleGroup group = 0)
        {
            var data = await apiLib.AdvancedSearchAsync(
                new AdvancedSearchInput() 
                { 
                    Title = title,
                    Genres = genre, 
                    Count = AdvancedSearchCount.Hundred, 
                    ReleaseDateFrom = releaseFrom, 
                    ReleaseDateTo = releaseTo,
                    Sort=sort,
                    USCertificates = certificate,
                    TitleGroups = group,
                }
            );
            return data.Results;
        }
        /// <summary>
        /// Поиск фильмов с похожим title
        /// </summary>
        /// <param name="title">Имя фильма</param>
        /// <returns>Task&lt;List&lt;SearchResult&gt;&gt;</returns>
        public async Task<List<SearchResult>> SearchByTitle(string title)
        {
            var data = await apiLib.SearchTitleAsync(title);
            return data.Results;
        }
        /// <summary>
        /// Png картинка репорт по данным фильма
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task&lt;byte[]&gt;</returns>
        public async Task<byte[]> ReportForMovie(string id = null)
        {
            var data = await apiLib.ReportBytesAsync(id,language: Language.en,false,Ratings: true);
            return data;
        }
        /// <summary>
        /// Фильмы которые показывают в кинотеатрах \ новинки
        /// </summary>
        /// <returns>Task&lt;List&lt;NewMovieDataDetail&gt;&gt;</returns>
        public async Task<List<NewMovieDataDetail>> GetNewMovies()
        {
            var data = await apiLib.InTheatersAsync();
            return data.Items;
        }

        /// <summary>
        /// Информация фильма по id
        /// </summary>
        /// <param name="id">Пример: tt1375666</param>
        /// <returns>Task&lt;TitleData&gt;</returns>
        public async Task<TitleData> MovieOrSeriesInfo(string id = null)
        {
            var data = await apiLib.TitleAsync(id,FullCast:true);
            return data;
        }
    }
}