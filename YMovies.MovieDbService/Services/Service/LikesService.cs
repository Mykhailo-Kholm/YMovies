using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;

namespace YMovies.MovieDbService.Services.Service
{
    public class LikesService
    {
        private readonly IRepository<Media> _mediaRepository;
        private readonly IRepository<User> _userRepository;

        public LikesService(MoviesContext context)
        {
            _mediaRepository = new MovieRepository(context);
            _userRepository = new UserRepository(context);
        }
        public void LikedMediaByUser(int userId, int mediaId)
        {
            var user = _userRepository.GetItem(userId);
            var media = _mediaRepository.GetItem(mediaId);
            user.LikedMedias.Add(media);
            _userRepository.UpdateItem(user);
        }

        public void LikeMedia(int id)
        {
            var media = _mediaRepository.GetItem(id);
            media.NumberOfLikes++;
            _mediaRepository.UpdateItem(media);
        }
        public void DislikeMedia(int id)
        {
            var media = _mediaRepository.GetItem(id);
            media.NumberOfDislikes++;
            _mediaRepository.UpdateItem(media);
        }
    }
}
