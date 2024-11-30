using BookNest.Models;

namespace BookNest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
            .HasData(new Category[]
            {
                new Category {Id = 1, Name = "Business"},
                new Category {Id = 2, Name = "Psychology"},
                new Category {Id = 3, Name = "History"},
                new Category {Id = 4, Name = "Science"},
                new Category {Id = 5, Name = "Sports"},
                new Category {Id = 6, Name = "Engineering"},
            });
            modelBuilder.Entity<Author>()
            .HasData(new Author[]
            {
                new Author {Id = 1 , Name = "Atossa Araxia Abrahamian"},
                new Author {Id = 2 , Name = "Lori Gottlieb"},
                new Author {Id = 3 , Name = "Dan Jones"},
                new Author {Id = 4 , Name = "Dava Sobel"},
                new Author {Id = 5 , Name = "Bree Wiley"},
                new Author {Id = 6 , Name = "Raymond A. Serway"},
                new Author {Id = 7 , Name = "John W. Jewett"},
                new Author {Id = 8 , Name = "Frances E. Reed"},
            });
            modelBuilder.Entity<BookAuthor>()
            .HasKey(e => new { e.BookId, e.AuthorId });

            base.OnModelCreating(modelBuilder);
        }
        
        
    }
            
}
            


     



