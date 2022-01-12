using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.Tests
{
    public class CountryServiceTests
    {
        private Country country;
        [SetUp]
        public void Setup()
        {
            country = new Country();
        }
        [Test]
        public void CountryService_TakeCountryForUpdate_UpdateCountryCalledOnce()
        {
            //Arrange 
            var service = new Mock<IService<Country>>();
            country.Id = 1;

            //Act 
            service.Object.UpdateItem(country);

            //Assert
            service.Verify(m => m.UpdateItem(country), Times.Once);
        }

        [Test]
        public void CountryService_TakeCountryForDelete_DeleteCountryCalledOnce()
        {
            //Arrange 
            var service = new Mock<IService<Country>>();
            country.Id = 1;

            //Act 
            service.Object.DeleteItem(country);

            //Assert
            service.Verify(m => m.DeleteItem(country), Times.Once);
        }

        [Test]
        public void GetItem_TakeIdForUser_ReturnCountry()
        {
            //Arrange 
            var service = new Mock<IService<Country>>();
            int id = 1;

            //Act 
            var result = service.Setup(a => a.GetItem(id)).Returns(country);
            //Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetItems_TakeItemsFromContext_ReturnListITems()
        {
            //Arrange 
            var service = new Mock<IService<Country>>();
            List<Country> countries = new List<Country>()
            {
                new Country()
                {
                    Id = 1
                },

                new Country()
                {
                    Id = 2
                }
            };

            //Act 
            var result = service.Setup(a => a.Items).Returns(countries);

            //Assert 
            Assert.IsNotNull(result);
        }

    }
}
