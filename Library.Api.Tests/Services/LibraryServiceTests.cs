using System.Data.Common;
using Library.Api.Controllers;
using Library.Api.Data;
using Library.Api.Models;
using Library.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Moq;
using Moq.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Library.Api.Tests.Services
{
    public class LibraryServiceTests
    {
        [Fact]
        public void GetAllAuthors_Returns_All_Authors_In_DB()
        {
            // Arrange
            var mockData = new List<Author>
            {
                new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Stephen King",
                    Label = "Mega Label"
                },
                new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Margaret Atwood",
                    Label = "Rebel Label"
                },
                new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Haruki Murakami",
                    Label = "Indie Label"
                }
            };
            var service = new LibraryService();
            var expected = mockData;

            // Act
            var result = service.GetAllAuthors();

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(expected, result);
        }

        [Fact]
        public void AddAuthor_Adds_New_Author_To_DB()
        {
            // Arrange
            var expected = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Test Author",
                Label = "Fake Label"
            };

            // Act
            var service = new LibraryService();
            var result = service.AddAuthor(expected);
            var fullList = service.GetAllAuthors();

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(expected, result);
            Assert.Contains<Author>(expected, fullList);
        }

        [Fact]
        public void AddAuthor_Throws_For_Existing_Author()
        {
            // Arrange
            var expected = new Exception("The author already exists in the database");

            // Act
            var service = new LibraryService();

            // Assert
            Assert.Throws<Exception>(
                () =>
                    service.AddAuthor(
                        new Author
                        {
                            Id = Guid.NewGuid(),
                            Name = "Haruki Murakami",
                            Label = "Indie Label"
                        }
                    )
            );
        }

        [Fact]
        public void DeleteAuthor_Removes_Author_From_DB()
        {
            // Arrange
            var expected = new Author
            {
                Id = Guid.Parse("d672f72f-806b-4e9c-8c0f-db397f636afb"),
                Name = "Haruki Murakami",
                Label = "Mega Label"
            };

            // Act
            var service = new LibraryService();
            var result = service.DeleteAuthor(expected);
            var updatedList = service.GetAllAuthors();

            // Assert
            Assert.DoesNotContain(result, updatedList);
        }

        [Fact]
        public void UpdateAuthor_Changes_Value_Of_Matching_Author()
        {
            // Arrange
            var expected = new Author
            {
                Id = Guid.Parse("d672f72f-806b-4e9c-8c0f-db397f636afb"),
                Name = "Haruki Murakami",
                Label = "Mega Label"
            };

            // Act
            var service = new LibraryService();
            var result = service.UpdateAuthor(expected);
            var updatedList = service.GetAllAuthors();

            // Assert
            // Assert.Contains<Author>(expected, updatedList);
            Assert.Equal(
                "Mega Label",
                updatedList.FirstOrDefault(a => a.Name == "Haruki Murakami")?.Label
            );
            Assert.Equivalent(expected, result);
        }
    }
}
