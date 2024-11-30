using BookNest.Settings;

namespace BookNest.ViewModels
{
    public class CreateBookFormViewModel : BookFormViewModel
    {
        public IFormFile Cover { get; set; } = default!;
    }
}
