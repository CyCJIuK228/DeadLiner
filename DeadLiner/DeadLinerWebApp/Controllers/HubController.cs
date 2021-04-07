using Microsoft.AspNetCore.Mvc;

namespace DeadLinerWebApp.Controllers
{
    public class HubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
