using System.Threading.Tasks;
using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeadLinerWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthorization _authorizationService;

        public AccountController(IAuthorization authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public IActionResult Login()
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _authorizationService.Login(model, HttpContext);
                if (user != null)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "A user with such credentials doesn't exist.");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _authorizationService.Logout(HttpContext);
            return RedirectToAction("Login", "Account");
        }
    }
}