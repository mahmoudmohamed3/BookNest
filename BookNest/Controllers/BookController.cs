using BookNest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;

namespace BookNest.Controllers
{
    public class BookController : Controller
    {
        private readonly ICategoriesService _CategoriesService;
        private readonly IAuthorService _AuthorService;
        private readonly IBookService _BookService;

        public BookController(ICategoriesService categoriesService, IAuthorService devicesService, IBookService gameService)
        {
            _CategoriesService = categoriesService;
            _AuthorService = devicesService;
            _BookService = gameService;
        }

        public IActionResult Details(int id)
        {
            var book = _BookService.GetById(id);

            if (book is null)
            {
                return NotFound();
            }

            return View(book);
        }

        public IActionResult Index()
        {
            var book = _BookService.GetAll();
            return View(book);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateBookFormViewModel viewmodel = new()
            {
                Categories = _CategoriesService.GetSelectList(),

                Authors = _AuthorService.GetSelectList()
            };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _CategoriesService.GetSelectList();

                model.Authors = _AuthorService.GetSelectList();

                return View(model);
            }

            await _BookService.Create(model);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _BookService.GetById(id);

            if (book is null)
                return NotFound();

            EditBookFormViewModel viewModel = new()
            {
                Id = id,
                Name = book.Name,
                Description = book.Description,
                CategoryId = book.CategoryId,
                SelectedAuthors = book.Authors.Select(d => d.AuthorId).ToList(),
                Categories = _CategoriesService.GetSelectList(),
                Authors = _AuthorService.GetSelectList(),
                CurrentCover = book.Cover,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _CategoriesService.GetSelectList();

                model.Authors = _AuthorService.GetSelectList();

                return View(model);
            }

            var book = await _BookService.Update(model);

            if (book is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var isDeleted = _BookService.Delete(id);

            if (isDeleted)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
