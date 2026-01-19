using Microsoft.AspNetCore.Mvc;

namespace Sim6.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
