using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.Tests
{
    public class CastServiceTests
    {
        private Cast cast;
        [SetUp]
        public void Setup()
        {
            cast = new Cast();
        }
        [Test]
        public void CastService_TakeCastForUpdate_UpdateCastCalledOnce()
        {
            //Arrange 
            var service = new Mock<IService<Cast>>();
            cast.Id = 1;

            //Act 
            service.Object.UpdateItem(cast);

            //Assert
            service.Verify(m => m.UpdateItem(cast), Times.Once);
        }

        [Test]
        public void CastService_TakeCastForDelete_DeleteCastCalledOnce()
        {
            //Arrange 
            var service = new Mock<IService<Cast>>();
            cast.Id = 1;

            //Act 
            service.Object.DeleteItem(cast);

            //Assert
            service.Verify(m => m.DeleteItem(cast), Times.Once);
        }

        [Test]
        public void GetItem_TakeIdForUser_ReturnCast()
        {
            //Arrange 
            var service = new Mock<IService<Cast>>();
            int id = 1;

            //Act 
            var result = service.Setup(a => a.GetItem(id)).Returns(cast);
            //Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetItems_TakeItemsFromContext_ReturnListITems()
        {
            //Arrange 
            var service = new Mock<IService<Cast>>();
            List<Cast> cast = new List<Cast>()
            {
                new Cast()
                {
                    Id = 1
                },

                new Cast()
                {
                    Id = 2
                }
            };

            //Act 
            var result = service.Setup(a => a.Items).Returns(cast);

            //Assert 
            Assert.IsNotNull(result);
        }

    }
}
