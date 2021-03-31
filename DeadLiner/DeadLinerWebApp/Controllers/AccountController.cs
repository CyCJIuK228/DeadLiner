using System.Threading.Tasks;
using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeadLinerWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public AccountController(IAuthorizationService authorizationService)
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
        public async Task<IActionResult> Login(LoginViewModel model)
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
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _authorizationService.Register(model);
                if (user != null)
                {
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError("", "A user with such credentials exist.");
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