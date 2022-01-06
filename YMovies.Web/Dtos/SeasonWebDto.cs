namespace YMovies.Web.DTOs
{
    public class SeasonWebDto
    {
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public int NumberOfEpisodes { get; set; }

        public int CurrentSeriesId { get; set; }
        public SeriesWebDto CurrentSeries { get; set; }
    }
}
