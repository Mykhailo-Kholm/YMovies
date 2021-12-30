namespace YMovies.Database.DTOs
{
    class SeasonDto
    {
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public int NumberOfEpisodes { get; set; }

        public int CurrentSeriesId { get; set; }
        public SeriesDto CurrentSeries { get; set; }
    }
}
