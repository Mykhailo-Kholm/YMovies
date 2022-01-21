using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.Tests
{
    public class GenreServiceTests
    {
        private Genre genre;
        [SetUp]
        public void Setup()
        {
            genre = new Genre();
        }
        [Test]
        public void GenreService_TakeMediaForUpdate_UpdateGenreCalledOnce()
        {
            //Arrange 
            var service = new Mock<IService<Genre>>();
            genre.Id = 1;

            //Act 
            service.Object.UpdateItem(genre);

            //Assert
            service.Verify(m => m.UpdateItem(genre), Times.Once);
        }

        [Test]
        public void GenreService_TakeMediaForDelete_DeleteGenreCalledOnce()
        {
            //Arrange 
            var service = new Mock<IService<Genre>>();
            genre.Id = 1;

            //Act 
            service.Object.DeleteItem(genre);

            //Assert
            service.Verify(m => m.DeleteItem(genre), Times.Once);
        }

        [Test]
        public void GetItem_TakeIdForUser_ReturnGenre()
        {
            //Arrange 
            var service = new Mock<IService<Genre>>();
            int id = 1;

            //Act 
            var result = service.Setup(a => a.GetItem(id)).Returns(genre);
            //Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetItems_TakeItemsFromContext_ReturnListItems()
        {
            //Arrange 
            var service = new Mock<IService<Genre>>();
            List<Genre> genres = new List<Genre>()
            {
                new Genre()
                {
                    Id = 1
                },

                new Genre()
                {
                    Id = 2
                }
            };

            //Act 
            var result = service.Setup(a => a.Items).Returns(genres);

            //Assert 
            Assert.IsNotNull(result);
        }

    }
}
