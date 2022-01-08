namespace YMovies.MovieDbService.DTOs
{
    public class StatisticDto
    {
        public int StatisticId { get; set; }
        public decimal Rating { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
    }
}
