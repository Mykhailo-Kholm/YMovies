using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.Tests
{
    public class MediaServiceTests
    {
        private Media media;
        [SetUp]
        public void Setup()
        {
            media = new Media();
        }
        [Test]
        public void MovieService_TakeMediaForUpdate_UpdateMovieCalledOnce()
        {
            //Arrange 
            var service = new Mock<IService<Media>>();
            media.MediaId = 1;

            //Act 
            service.Object.UpdateItem(media);

            //Assert
            service.Verify(m => m.UpdateItem(media), Times.Once);
        }

        [Test]
        public void MovieService_TakeMediaForDelete_DeleteMovieCalledOnce()
        {
            //Arrange 
            var service = new Mock<IService<Media>>();
            media.MediaId = 1;

            //Act 
            service.Object.DeleteItem(media);

            //Assert
            service.Verify(m => m.DeleteItem(media), Times.Once);
        }

        [Test]
        public void GetItem_TakeIdForUser_ReturnMovie()
        {
            //Arrange 
            var service = new Mock<IService<Media>>();
            int id = 1;

            //Act 
            var result = service.Setup(a => a.GetItem(id)).Returns(media);
            //Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetItems_TakeItemsFromContext_ReturnListITems()
        {
            //Arrange 
            var service = new Mock<IService<Media>>();
            List<Media> medias = new List<Media>()
            {
                new Media()
                {
                    MediaId = 1
                },

                new Media()
                {
                    MediaId = 2
                }
            };

            //Act 
            var result = service.Setup(a => a.Items).Returns(medias);

            //Assert 
            Assert.IsNotNull(result);
        }

    }
}
