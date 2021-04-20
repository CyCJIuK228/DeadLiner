using DeadLinerWebApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeadLinerWebApp.Controllers
{
    public class HubController : Controller
    {
        private readonly IHubService _hubService;

        public HubController(IHubService service)
        {
            _hubService = service;
        }

        public IActionResult Index(string title)
        {
            var tasks =_hubService.GetTasks(title, User.Identity.Name);
            return View(tasks);
        }
    }
}
