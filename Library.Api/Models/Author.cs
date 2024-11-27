namespace Library.Api.Models
{
    public class Author
    {
        public Guid Id { set; get; }
        public string Name { set; get; } = String.Empty;
        public string Label {set;get;} = String.Empty;
    }
}
