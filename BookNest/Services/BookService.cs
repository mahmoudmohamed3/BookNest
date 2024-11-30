using BookNest.Settings;

namespace BookNest.Services
{
    public class BookService: IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;
        public BookService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }

        public IEnumerable<Book> GetAll()
        {
            var Book = _context.Book
                .Include(g => g.Category)
                .Include(g => g.Authors)
                .ThenInclude(d => d.Author)
                .AsNoTracking()
                .ToList();
            return Book;
        }

        public Book? GetById(int id)
        {
            var Book = _context.Book
                .Include(g => g.Category)
                .Include(g => g.Authors)
                .ThenInclude(d => d.Author)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
            return Book;
        }

        public async Task Create(CreateBookFormViewModel model)
        {
            var CoverName = await SaveCover(model.Cover);

            Book Book = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = CoverName,
                Authors = model.SelectedAuthors.Select(d => new BookAuthor { AuthorId = d }).ToList()
            };

            _context.Book.Add(Book);
            _context.SaveChanges();

        }

        public async Task<Book?> Update(EditBookFormViewModel model)
        {
            var book = _context.Book
                .Include(b => b.Authors)
                .SingleOrDefault(b => b.Id == model.Id);

            if (book is null)
                return null;
            var hasNewCover = model.Cover != null;
            var oldCover = book.Cover;

            book.Name = model.Name;
            book.Description = model.Description;
            book.CategoryId = model.CategoryId;
            book.Authors = model.SelectedAuthors.Select(a => new BookAuthor { AuthorId = a}).ToList();

            if (hasNewCover)
            {
                book.Cover = await SaveCover(model.Cover!);
            }

            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagesPath, oldCover);
                    File.Delete(cover);
                }

                return book;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, book.Cover);
                File.Delete(cover);
                return null;
            }

        }

        private async Task<string> SaveCover (IFormFile Cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";

            var path = Path.Combine(_imagesPath, CoverName);

            using var stream = File.Create(path);
            await Cover.CopyToAsync(stream);

            return CoverName;
        }


        public bool Delete(int id)
        {
            var book = _context.Book
                .Include(b => b.Authors)
                .SingleOrDefault(b => b.Id == id);

            if (book == null)
                return false;

            if (!string.IsNullOrEmpty(book.Cover))
            {
                var coverPath = Path.Combine(_imagesPath, book.Cover);
                if (File.Exists(coverPath))
                {
                    File.Delete(coverPath);
                }
            }

            _context.Book.Remove(book);
            var effectedRows = _context.SaveChanges();

            return effectedRows > 0;
        }

    }
}
