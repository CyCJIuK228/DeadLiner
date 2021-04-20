using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.PL.Models;
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
            var model = _hubService.GetHubs(User.Identity.Name);
            return View(model);
        }

        [HttpGet]
        public IActionResult AddHub()
        {
            return PartialView("CreateHub");
        }

        [HttpPost]
        public IActionResult AddHub(HubModel model)
        {
            _hubService.CreateHub(model.Title, model.Description, User.Identity.Name);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }
    }
}