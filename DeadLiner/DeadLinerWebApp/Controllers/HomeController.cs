using DeadLinerWebApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DeadLinerWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHubService _hubService;
        public HomeController(IHubService hubService)
        {
            _hubService = hubService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _hubService.GetHubs();
            return View(model);
        }
    }
}