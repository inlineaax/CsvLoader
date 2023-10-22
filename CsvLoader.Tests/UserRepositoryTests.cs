using Domain.Entities;
using Infra.Context;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CsvLoader.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public void SearchUsers_ShouldReturnFilteredResults()
        {
            // Arrange
            var contextMock = new Mock<ICsvLoaderContext>();
            var repository = new UserRepository(contextMock.Object);

            var testData = new List<User>
        {
            new User { Name = "John", City = "City1", Country = "Country1", FavoriteSport = "Sport1" },
            new User { Name = "Alice", City = "City2", Country = "Country2", FavoriteSport = "Sport2" }
        };

            var query = "John";

            var dbSetMock = new Mock<DbSet<User>>();

            dbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(testData.AsQueryable().Provider);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(testData.AsQueryable().Expression);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(testData.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => testData.AsQueryable().GetEnumerator());

            contextMock.Setup(c => c.Users).Returns(dbSetMock.Object);

            // Act
            var results = repository.SearchUsers(query);

            // Assert
            Assert.NotEmpty(results);
        }

        [Fact]
        public void UserExists_ShouldReturnTrueIfExists()
        {
            // Arrange
            var contextMock = new Mock<ICsvLoaderContext>();
            var repository = new UserRepository(contextMock.Object);

            var testData = new List<User>
        {
            new User { Name = "John", City = "City1", Country = "Country1", FavoriteSport = "Sport1" },
            new User { Name = "Alice", City = "City2", Country = "Country2", FavoriteSport = "Sport2" }
        };

            var name = "John";
            var city = "City1";
            var country = "Country1";
            var sport = "Sport1";

            var dbSetMock = new Mock<DbSet<User>>();

            dbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(testData.AsQueryable().Provider);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(testData.AsQueryable().Expression);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(testData.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => testData.AsQueryable().GetEnumerator());

            contextMock.Setup(c => c.Users).Returns(dbSetMock.Object);

            // Act
            var userExists = repository.UserExists(name, city, country, sport);

            // Assert
            Assert.True(userExists);
        }
    }

}
