using System;
using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NToastNotify;

namespace DeadLinerWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHubService _hubService;
        private readonly IToastNotification _toastNotification;
        private readonly IProfileService _profileService;

        public HomeController(IHubService hubService, IProfileService profileService, IToastNotification toast)
        {
            _hubService = hubService;
            _profileService = profileService;
            _toastNotification = toast;
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
            try
            {
                _hubService.CreateHub(model.Title, model.Description, User.Identity.Name);
                _toastNotification.AddSuccessToastMessage("Hub has successfully created.");
            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage("Hub with such a name already exists.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteHub(string title)
        {
            _hubService.DeleteHub(title);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var model = _profileService.GetUserInfo(User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public IActionResult Profile(UserInfoViewModel user)
        {
            bool IsSuccessful = true;
            try
            {
                _profileService.UpdateUserInfo(user, User.Identity.Name);
            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage(e.Message);
                IsSuccessful = false;
            }

            if(IsSuccessful)
                _toastNotification.AddSuccessToastMessage("Profile has been successfully updated.");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AcceptJoin(string title)
        {
            _hubService.AcceptJoinToHub(User.Identity.Name, title);
            _toastNotification.AddInfoToastMessage($"Congrats! Now you are a member of {title} hub!");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult RejectJoin(string title)
        {
            _hubService.RejectJoinToHub(User.Identity.Name, title);
            return RedirectToAction("Index");
        }
    }
}