using Microsoft.AspNetCore.Mvc;

namespace Sim6.Areas.Admin.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
