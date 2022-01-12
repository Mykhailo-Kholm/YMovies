namespace YMovies.Web.Models.MoviesInfoViewModel
{
    public class IndexMediaViewModel
    {
        public int MediaId { get; set; }        
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public decimal ImdbRating { get; set; }
        public string PosterUrl { get; set; }
    }
}