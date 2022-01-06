namespace YMovies.Web.DTOs
{
    class SeasonWebDto
    {
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public int NumberOfEpisodes { get; set; }

        public int CurrentSeriesId { get; set; }
        public SeriesWebDto CurrentSeries { get; set; }
    }
}
