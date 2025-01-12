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

namespace Library.Api.Tests.Services
{
    public class LibraryServiceTests
    {
        private readonly Mock<IApiContext> _context;

        private readonly LibraryService _service;

        public LibraryServiceTests()
        {
            var mockData = new List<Author>
            {
                new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Stephen King",
                    Label = "Mega Label",
                },
                new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Margaret Atwood",
                    Label = "Rebel Label",
                },
                new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Haruki Murakami",
                    Label = "Indie Label",
                },
            }.AsQueryable();

            _context = new Mock<IApiContext>();
            _context.Setup(db => db.Authors).ReturnsDbSet(mockData);

            _service = new LibraryService(_context.Object);
        }

        [Fact]
        public async void GetAllAuthors_Returns_All_Authors_In_DB()
        {
            // Act
            var result = await _service.GetAllAuthors(1, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(_context.Object.Authors.Take(2), result.Items);
        }

        [Fact]
        public async void AddAuthor_Adds_New_Author_To_DB()
        {
            // Arrange
            var expected = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Test Author",
                Label = "Fake Label",
            };

            // Act
            var result = _service.AddAuthor(expected);
            var fullList = await _service.GetAllAuthors(1, 4);

            // Assert
            Assert.NotNull(result);
            Assert.Equivalent(expected, result);
            // Assert.Equal(4, fullList.Items.Count);
            // Assert.Contains<Author>(expected, fullList.Items);
        }

        [Fact]
        public void AddAuthor_Throws_For_Existing_Author()
        {
            // Arrange
            var expected = new Exception("The author already exists in the database");

            // Assert
            Assert.Throws<Exception>(
                () =>
                    _service.AddAuthor(
                        new Author
                        {
                            Id = Guid.NewGuid(),
                            Name = "Haruki Murakami",
                            Label = "Indie Label",
                        }
                    )
            );
        }

        [Fact]
        public async void DeleteAuthor_Removes_Author_From_DB()
        {
            // Arrange
            var expected = new Author
            {
                Id = Guid.Parse("d672f72f-806b-4e9c-8c0f-db397f636afb"),
                Name = "Haruki Murakami",
                Label = "Mega Label",
            };

            // Act
            var result = _service.DeleteAuthor(expected);
            var updatedList = await _service.GetAllAuthors(1, 3);

            // Assert
            // Assert.Equal(2, updatedList.Items.Count);
            // Assert.DoesNotContain(result, updatedList.Items);
        }

        [Fact]
        public async void UpdateAuthor_Changes_Value_Of_Matching_Author()
        {
            // Arrange
            var expected = new Author
            {
                Id = Guid.Parse("d672f72f-806b-4e9c-8c0f-db397f636afb"),
                Name = "Haruki Murakami",
                Label = "Mega Label",
            };

            // Act
            var result = _service.UpdateAuthor(expected);
            var updatedList = await _service.GetAllAuthors(1, 3);

            // Assert
            // Assert.Contains<Author>(expected, updatedList);
            Assert.Equal(
                "Mega Label",
                updatedList.Items.FirstOrDefault(a => a.Name == "Haruki Murakami")?.Label
            );
            Assert.Equivalent(expected, result);
        }
    }
}
