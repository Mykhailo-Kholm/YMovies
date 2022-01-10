using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;

namespace YMovies.MovieDbService.Services.Service
{
    public class WatchService
    {
        private readonly IRepository<Media> _mediaRepository;
        private readonly IRepository<User> _userRepository;

        public WatchService(MoviesContext context)
        {
            _mediaRepository = new MovieRepository(context);
            _userRepository = new UserRepository(context);
        }
        public void WatchedMediaByUser(int userId, int mediaId)
        {
            var user = _userRepository.GetItem(userId);
            var media = _mediaRepository.GetItem(mediaId);
            user.WatchedMedias.Add(media);
            _userRepository.UpdateItem(user);
        }
    }
}
