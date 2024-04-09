namespace Library.Api.Models
{
    public class Book
    {
        public Guid Id { set; get; }
        public string Title { set; get; } = String.Empty;
        public string Author { set; get; } = String.Empty;
    }
}
