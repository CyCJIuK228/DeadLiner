using Microsoft.AspNetCore.Mvc;

namespace DeadLinerWebApp.Controllers
{
    public class StartPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
