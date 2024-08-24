using Microsoft.AspNetCore.Mvc;

namespace BookStore.MVC.Controllers
{
    public class TabulatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
