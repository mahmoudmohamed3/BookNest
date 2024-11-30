using BookNest.Models;
using BookNest.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookNest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;

        public HomeController(IBookService gamesService)
        {
            _bookService = gamesService;
        }

        public IActionResult Index()
        {
            var book = _bookService.GetAll();
            return View(book);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
