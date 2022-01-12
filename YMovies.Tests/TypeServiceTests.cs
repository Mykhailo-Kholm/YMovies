using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.Tests
{
    public class TypeServiceTests
    {
        private Type type;
        [SetUp]
        public void Setup()
        {
            type = new Type();
        }
        [Test]
        public void TypeService_TakeMediaForUpdate_UpdateTypeCalledOnce()
        {
            //Arrange 
            var service = new Mock<IService<Type>>();
            type.Id = 1;

            //Act 
            service.Object.UpdateItem(type);

            //Assert
            service.Verify(m => m.UpdateItem(type), Times.Once);
        }

        [Test]
        public void TypeService_TakeTypeForDelete_DeleteTypeCalledOnce()
        {
            //Arrange 
            var service = new Mock<IService<Type>>();
            type.Id = 1;

            //Act 
            service.Object.DeleteItem(type);

            //Assert
            service.Verify(m => m.DeleteItem(type), Times.Once);
        }

        [Test]
        public void GetItem_TakeIdForUser_ReturnType()
        {
            //Arrange 
            var service = new Mock<IService<Type>>();
            int id = 1;

            //Act 
            var result = service.Setup(a => a.GetItem(id)).Returns(type);
            //Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetItems_TakeItemsFromContext_ReturnListITems()
        {
            //Arrange 
            var service = new Mock<IRepository<Type>>();
            List<Type> types = new List<Type>()
            {
                new Type()
                {
                    Id = 1
                },

                new Type()
                {
                    Id = 2
                }
            };

            //Act 
            var result = service.Setup(a => a.Items).Returns(types);

            //Assert 
            Assert.IsNotNull(result);
        }

    }
}
