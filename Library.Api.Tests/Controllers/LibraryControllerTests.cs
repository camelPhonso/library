using Library.Api.Controllers;
using Library.Api.Data;
using Library.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Library.Api.Tests.Controllers
{
    public class LibraryControllerTests
    {
        [Fact]
        public void GetAllAuthors_Returns_All_Authors_In_DB()
        {
            // Arrange
            var mockData = new List<Author>
            {
                new Author { Id = Guid.NewGuid(), Name = "Clive Barker" },
                new Author { Id = Guid.NewGuid(), Name = "Afonso Cruz" }
            }.AsQueryable();
            var testContext = new Mock<IApiContext>();
            testContext.Setup(db => db.Authors).ReturnsDbSet(mockData);
            var testController = new LibraryController(testContext.Object);

            // Act
            var result = testController.GetAllAuthors();
            var expected = new JsonResult(testContext.Object.Authors.ToList());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.Equal(
                JsonConvert.SerializeObject(expected),
                JsonConvert.SerializeObject(result)
            );
        }
    }
}
