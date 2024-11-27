using Library.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Services
{
    public class LibraryService : ILibraryService
    {
        private List<Author> _authors { get; set; } =
            new List<Author>
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
                    Id = Guid.Parse("d672f72f-806b-4e9c-8c0f-db397f636afb"),
                    Name = "Haruki Murakami",
                    Label = "Indie Label"
                }
            };
        private List<Book> _books { get; set; } =
            new List<Book>
            {
                new Book
                {
                    Id = Guid.NewGuid(),
                    Author = "Haruki Murakami",
                    Title = "Kafka On The Shore"
                },
                new Book
                {
                    Id = Guid.Parse("55929dce-8488-4cb1-bbea-39c0d757c3eb"),
                    Author = "Haruki Murakami",
                    Title = "Norwegian Wood"
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Author = "Margaret Atwood",
                    Title = "The Testaments"
                }
            };

        public List<Author> GetAllAuthors()
        {
            var result = _authors.ToList();

            if (result is null)
                throw new Exception("No Authors available");

            return result;
        }

        public Author AddAuthor(Author newAuthor)
        {
            var authorInDb = _authors.FirstOrDefault(author => author.Name == newAuthor.Name);

            if (authorInDb is null)
            {
                _authors.Add(newAuthor);
                return newAuthor;
            }
            else
            {
                throw new Exception("The author already exists in the database");
            }
        }

        public Author DeleteAuthor(Author author)
        {
            var authorInDb = _authors.FirstOrDefault(a => a.Name == author.Name);

            if (authorInDb is null)
                throw new Exception("Author not found in DB");

            _authors.Remove(authorInDb);

            return authorInDb;
        }

        public Author UpdateAuthor(Author author)
        {
            var authorInDb = _authors.FirstOrDefault(a => a.Name == author.Name);

            if (authorInDb is null)
                throw new Exception("Author not found");

            // _authors.Remove(authorInDb);
            // _authors.Add(author);

            authorInDb.Label = author.Label;

            return author;
        }
    }
}
