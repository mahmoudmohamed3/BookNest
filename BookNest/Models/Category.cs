namespace BookNest.Models
{
    public class Category : BaseEntity
    {
        public ICollection<Book> books = new List<Book>();
    }
}
