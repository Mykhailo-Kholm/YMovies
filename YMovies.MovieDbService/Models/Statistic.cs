namespace YMovies.MovieDbService.Models
{
    public class Statistic
    {
        public int StatisticId { get; set; } 
        public decimal Rating { get; set; } 
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }

        public Movie Movie { get; set; }
        public Series Series { get; set; }
    }
}
