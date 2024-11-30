using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookNest.ViewModels
{
    public class BookFormViewModel
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Authors")]
        public List<int> SelectedAuthors { get; set; } = default!;
        public IEnumerable<SelectListItem> Authors { get; set; } = Enumerable.Empty<SelectListItem>();

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;
    }
}
