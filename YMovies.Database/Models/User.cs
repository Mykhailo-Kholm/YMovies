namespace YMovies.Database.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public Liked Liked { get; set; }
        public Watched Watched { get; set; }
    }
}
