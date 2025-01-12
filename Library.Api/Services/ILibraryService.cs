using Library.Api.Helpers;
using Library.Api.Models;

namespace Library.Api.Services
{
    public interface ILibraryService
    {
        public Task<PagedList<Author>> GetAllAuthors(int pageIndex, int pageSize);
        public Author AddAuthor(Author newAuthor);
        public Author DeleteAuthor(Author author);
        public Author UpdateAuthor(Author author);
    }
}
