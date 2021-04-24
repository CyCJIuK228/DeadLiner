using System;
using System.Text;
using System.Threading.Tasks;
using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.PL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace DeadLinerWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IToastNotification _toastNotification;

        public AccountController(IAuthorizationService authorizationService, IToastNotification toast)
        {
            _authorizationService = authorizationService;
            _toastNotification = toast;
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

                _toastNotification.AddErrorToastMessage("A user with such credentials doesn't exist.");
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
                    _toastNotification.AddSuccessToastMessage("User has been successfully registered.");
                    return RedirectToAction("Login");
                }

                _toastNotification.AddErrorToastMessage("A user with such credentials exist.");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _authorizationService.ForgotPassword(model);
                    HttpContext.Session.Set("email", Encoding.ASCII.GetBytes(model.Email));
                    return RedirectToAction("EnterCode");
                }
                catch (Exception)
                {
                    _toastNotification.AddErrorToastMessage("A user with such credentials doesn't exist.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EnterCode()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnterCode(CodeViewModel model)
        {
            if (ModelState.IsValid &&
                model.ActualCode.Equals(_authorizationService.GetRecoveryCode(Encoding.ASCII.GetString(HttpContext.Session.Get("email")))))
            {
                return RedirectToAction("RecoverPassword");
            }

            _toastNotification.AddErrorToastMessage("Incorrect code inputted.");
            return View(model);
        }

        [HttpGet]
        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecoverPassword(RecoveryPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Email = Encoding.ASCII.GetString(HttpContext.Session.Get("email"));
                _authorizationService.ReplacePassword(model);
                _toastNotification.AddSuccessToastMessage("Password has been successfully changed.");
                return RedirectToAction("Login", "Account");
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