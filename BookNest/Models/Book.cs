namespace BookNest.Models
{
    public class Book : BaseEntity
    {
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Cover { get; set; } = string.Empty;
        public int CategoryId { get; set; }


        public Category Category { get; set; } = default!;
        public ICollection<BookAuthor> Authors { get; set; } = new List<BookAuthor>();

    }
}
