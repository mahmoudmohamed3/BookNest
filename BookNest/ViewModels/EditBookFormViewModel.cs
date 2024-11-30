namespace BookNest.ViewModels
{
    public class EditBookFormViewModel : BookFormViewModel
    {
        public int Id { get; set; }

        public string? CurrentCover { get; set; }

        public IFormFile? Cover { get; set; } = default!;
    }
}
