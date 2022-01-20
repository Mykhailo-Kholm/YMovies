using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;

namespace YMovies.Tests
{
    public class Tests
    {
        private Media media;
        [SetUp]
        public void Setup()
        {
             media = new Media();
        }

        [Test]
        public void GetItem_TakeId_ReturnFirstMovie()
        {
            //Arrange
            var service = new Mock<IRepository<Media>>();
            int id = 1;

            //Act
            var result = service.Setup(a => a.GetItem(id)).Returns(media);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetItem_TakeId_CheckMovieExist()
        {
            //Arrange 
            var service = new Mock<IRepository<Media>>();
            int id = 1;

            //Act 
            service.Object.GetItem(id);

            //Assert
            service.Verify(m => m.GetItem(id), Times.Once);

        }

        [Test]
        public void DeleteItem_TakeIdOfMovie_DeleteMedia()
        {
            //Arrange 
            var service = new Mock<IRepository<Media>>();
            int id = 1;

            //Act 
            service.Object.DeleteItem(id);

            service.Verify(m => m.DeleteItem(id), Times.Once);
        }
        [Test]
        public void UpdateItem_TakeIdOfMovie_UpdateMedia()
        {
            //Arrange 
            var service = new Mock<IRepository<Media>>();

            media.MediaId = 1;

            //Act 
            service.Object.UpdateItem(media);

            //Assert
            service.Verify(m => m.UpdateItem(media), Times.Once);
        }
        [Test]
        public void GetItems_TakeItemsFromContext_ReturnListITems()
        {
            //Arrange 
            var service = new Mock<IRepository<Media>>();
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

        [Test]
        public void GetMediaByParams_FilterItems_ReturnsFilterdMovies()
        {

        }
    }
}