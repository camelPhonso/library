using Library.Api.Data;
using Library.Api.Helpers;
using Library.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IApiContext _context;

        public LibraryService(IApiContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Author>> GetAllAuthors(int pageIndex, int pageSize)
        {
            IQueryable<Author> query = _context.Authors;

            var paginatedResult = await PagedList<Author>.CreateAsync(query, pageIndex, pageSize);

            if (paginatedResult is null)
                throw new Exception("No Authors available");

            return paginatedResult;
        }

        public Author AddAuthor(Author newAuthor)
        {
            var authorInDb = _context.Authors.FirstOrDefault(author =>
                author.Name == newAuthor.Name
            );

            if (authorInDb is null)
            {
                _context.Authors.Add(newAuthor);
                _context.SaveChanges();
                return newAuthor;
            }
            else
            {
                throw new Exception("The author already exists in the database");
            }
        }

        public Author DeleteAuthor(Author author)
        {
            var authorInDb = _context.Authors.FirstOrDefault(a => a.Name == author.Name);

            if (authorInDb is null)
                throw new Exception("Author not found in DB");

            _context.Authors.Remove(authorInDb);
            _context.SaveChanges();

            return authorInDb;
        }

        public Author UpdateAuthor(Author author)
        {
            var authorInDb = _context.Authors.FirstOrDefault(a => a.Name == author.Name);

            if (authorInDb is null)
                throw new Exception("Author not found");

            // _authors.Remove(authorInDb);
            // _authors.Add(author);

            authorInDb.Label = author.Label;

            return author;
        }
    }
}
