using Library.Api.Models;

namespace Library.Api.Services
{
  public interface ILibraryService
  {
    public List<Author> GetAllAuthors();
    public Author AddAuthor(Author newAuthor);
    public Author DeleteAuthor(Author author);
    public Author UpdateAuthor(Author author);
    public Author GetAuthor(number id);
  }
}