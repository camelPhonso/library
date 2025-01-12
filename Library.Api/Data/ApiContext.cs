using Library.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Data
{
    public interface IApiContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public int SaveChanges();
    }

    public class ApiContext : DbContext, IApiContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public ApiContext(DbContextOptions options)
            : base(options) { }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Author>()
                .HasData(
                    new Author { Id = Guid.NewGuid(), Name = "Stephen King" },
                    new Author { Id = Guid.NewGuid(), Name = "Margaret Atwood" },
                    new Author { Id = Guid.NewGuid(), Name = "Haruki Murakami" }
                );

            modelBuilder
                .Entity<Book>()
                .HasData(
                    new Book
                    {
                        Id = Guid.NewGuid(),
                        Author = "Haruki Murakami",
                        Title = "Kafka On The Shore",
                    },
                    new Book
                    {
                        Id = Guid.NewGuid(),
                        Author = "Haruki Murakami",
                        Title = "Norwegian Wood",
                    },
                    new Book
                    {
                        Id = Guid.NewGuid(),
                        Author = "Margaret Atwood",
                        Title = "The Testaments",
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
