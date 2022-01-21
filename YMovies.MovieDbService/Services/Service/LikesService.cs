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
        public void LikedMediaByUser(string userId, int mediaId)
        {
            var user = _userRepository.GetItem(userId);
            var media = _mediaRepository.GetItem(mediaId);
            if (user.LikedMedias == null)
                user.LikedMedias = new List<Media>();

            if (user.LikedMedias.Contains(media)) return;

            user.LikedMedias.Add(media);
            if (user.DislikedMedias?.Contains(media) ?? false)
            {
                user.DislikedMedias.Remove(media);
                media.NumberOfDislikes--;
            }

            _userRepository.UpdateItem(user);
            media.NumberOfLikes++;
            _mediaRepository.UpdateItem(media);
        }
        public void DislikedMediaByUser(string userId, int mediaId)
        {
            var user = _userRepository.GetItem(userId);
            var media = _mediaRepository.GetItem(mediaId);
            if (user.DislikedMedias == null)
                user.DislikedMedias = new List<Media>();

            if (user.DislikedMedias.Contains(media)) return;

            user.DislikedMedias.Add(media);
            if (user.LikedMedias?.Contains(media) ?? false)
            {
                user.LikedMedias.Remove(media);
                media.NumberOfLikes--;
            }
            _userRepository.UpdateItem(user);
            media.NumberOfDislikes++;
            _mediaRepository.UpdateItem(media);
        }
        public bool IsLiked(string userId, int mediaId)
        {
            var user = _userRepository.GetItem(userId);
            var media = _mediaRepository.GetItem(mediaId);
            return user.LikedMedias?.Contains(media) ?? false;
        }
        public bool IsDisliked(string userId, int mediaId)
        {
            var user = _userRepository.GetItem(userId);
            var media = _mediaRepository.GetItem(mediaId);
            return user.DislikedMedias?.Contains(media) ?? false;
        }


    }
}
