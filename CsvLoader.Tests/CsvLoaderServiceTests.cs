using Application.Services;
using Domain.Entities;
using Domain.IRepositories;
using Moq;

namespace CsvLoader.Tests
{
    public class CsvLoaderServiceTests
    {
        [Fact]
        public void SaveUsers_ShouldCallUserRepositorySaveUsers()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var service = new CsvLoaderService(userRepositoryMock.Object);

            var users = new List<User> { new User() };

            // Act
            service.SaveUsers(users);

            // Assert
            userRepositoryMock.Verify(repo => repo.SaveUsers(users), Times.Once);
        }
    }

}
