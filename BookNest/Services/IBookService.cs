
namespace BookNest.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book? GetById(int id);
        public Task Create(CreateBookFormViewModel model);
        public Task<Book?> Update(EditBookFormViewModel model);

        public bool Delete(int id);
    }
}
