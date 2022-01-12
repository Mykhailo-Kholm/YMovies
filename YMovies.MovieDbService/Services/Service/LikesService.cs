using System.Collections.Generic;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.Repository;

namespace YMovies.MovieDbService.Services.Service
{
    public class LikesService
    {
        private readonly MovieRepository _mediaRepository;
        private readonly UserRepository _userRepository;

        public LikesService(MoviesContext context)
        {
            _mediaRepository = new MovieRepository(context);
            _userRepository = new UserRepository(context);
        }
        public bool LikedMediaByUser(string userId, int mediaId)
        {
            var user = _userRepository.GetItem(userId);
            var media = _mediaRepository.GetItem(mediaId);
            if (!user.LikedMedias.Contains(media))
            {
                user.LikedMedias.Add(media);
                _userRepository.UpdateItem(user);
                LikeMedia(mediaId);
                return true;
            }
            return false;
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
        public bool IsLiked(string userId, int mediaId)
        {
            var user = _userRepository.GetItem(userId);
            if (user.LikedMedias == null)
            {
                user.LikedMedias = new List<Media>();
                return false;
            }
            var media = _mediaRepository.GetItem(mediaId);
            return user.LikedMedias.Contains(media);
        }
    }
}
