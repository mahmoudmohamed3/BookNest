using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookNest.Services
{
    public interface IAuthorService
    {
        public IEnumerable<SelectListItem> GetSelectList();

    }
}
