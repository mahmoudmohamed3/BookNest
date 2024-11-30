using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookNest.Services
{
    public interface ICategoriesService
    {
        public IEnumerable<SelectListItem> GetSelectList();

    }
}
