using System.Collections.Generic;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Models;
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
        public void WatchedMediaByUser(string userId, int mediaId)
        {
            var user = _userRepository.GetItem(userId);
            if (user.WatchedMedias == null)
                user.WatchedMedias = new List<Media>();
            var media = _mediaRepository.GetItem(mediaId);
            if (user.WatchedMedias.Contains(media)) return;
            user.WatchedMedias.Add(media);
            _userRepository.UpdateItem(user);
        }

        public bool IsWatched(string userId, int mediaId)
        {
            var user = _userRepository.GetItem(userId);
            var media = _mediaRepository.GetItem(mediaId);
            return user.DislikedMedias?.Contains(media) ?? false;
        }
    }
}
