using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;

namespace YMovies.MovieDbService.Services.Service
{
    public class WatchService
    {
        private readonly MovieRepository _mediaRepository;
        private readonly UserRepository _userRepository;

        public WatchService(MoviesContext context)
        {
            _mediaRepository = new MovieRepository(context);
            _userRepository = new UserRepository(context);
        }
        public bool WatchedMediaByUser(string userId, int mediaId)
        {
            var user = _userRepository.GetItem(userId);
            var media = _mediaRepository.GetItem(mediaId);
            if (!user.WatchedMedias.Contains(media))
            {
                user.WatchedMedias.Add(media);
                _userRepository.UpdateItem(user);
                return true;
            }

            return false;
        }
    }
}
