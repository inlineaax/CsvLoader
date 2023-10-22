using Application.IServices;
using CsvLoader.Controllers;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CsvLoader.Tests
{
    public class CsvLoaderControllerTests
    {
        [Fact]
        public void UploadCsv_WithValidFile_ShouldReturnOkResult()
        {
            // Arrange
            var csvLoaderServiceMock = new Mock<ICsvLoaderService>();
            var controller = new CsvLoaderController(csvLoaderServiceMock.Object);
            var csvFile = new Mock<IFormFile>();

            // Configure o comportamento esperado da chamada para ReadCsvFile
            csvLoaderServiceMock.Setup(service => service.ReadCsvFile(It.IsAny<Stream>()))
                .Returns((new List<User>(), new List<User>()));

            // Act
            var result = controller.UploadCsv(csvFile.Object);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UploadCsv_WithDuplicateUsers_ShouldReturnOkResultWithMessage()
        {
            // Arrange
            var csvLoaderServiceMock = new Mock<ICsvLoaderService>();
            var controller = new CsvLoaderController(csvLoaderServiceMock.Object);
            var csvFile = new Mock<IFormFile>();
            var duplicateUsers = new List<User> { new User { Name = "John Doe" } };
            csvLoaderServiceMock.Setup(service => service.ReadCsvFile(It.IsAny<Stream>()))
                .Returns((new List<User>(), duplicateUsers));

            // Act
            var result = controller.UploadCsv(csvFile.Object);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

    }


}