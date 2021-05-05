using System;
using System.Collections.Generic;
using System.Linq;
using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.PL;
using DeadLinerWebApp.PL.Models;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace DeadLinerWebApp.Controllers
{
    public class HubController : Controller
    {
        private readonly IHubService _hubService;
        private readonly IToastNotification _toastNotification;

        public HubController(IHubService service, IToastNotification toastNotification)
        {
            _hubService = service;
            _toastNotification = toastNotification;
        }

        public IActionResult Index([FromQuery] string title)
        {
            var tasks = _hubService.GetTasks(title, User.Identity.Name);
            tasks.UsersName = _hubService.GetUsersInHub(title);
            tasks.Invites = _hubService.GeInvitesInHub(title);
            return View(tasks);
        }

        [HttpGet]
        public IActionResult InviteUser(string title, string name)
        {
            return View(new InviteUserViewModel { Title = title, UserName = name ?? "" });
        }

        [HttpPost]
        public IActionResult InviteUser(InviteUserViewModel model)
        {
            if (model.UserName == null)
            {
                _toastNotification.AddErrorToastMessage("Firstly try to search this user.");
                return RedirectToAction("InviteUser", "Hub", new { title = model.Title });
            }

            _hubService.InviteUser(model);
            _toastNotification.AddSuccessToastMessage("User invitation successfully sent");
            return RedirectToAction("Index", "Hub", new { title = model.Title });
        }


        [HttpGet]
        public IActionResult FindUser(InviteUserViewModel model)
        {
            var user = _hubService.FindUser(model.UserName);
            if (user == null)
            {
                _toastNotification.AddErrorToastMessage("Such a user does not exist.");
            }
            else
            {
                _toastNotification.AddSuccessToastMessage("Such user exist.");
            }

            return RedirectToAction("InviteUser", "Hub", new { title = model.Title, name = model.UserName });
        }

        [HttpPost]
        public IActionResult AddTask(TaskViewModel model)
        {
            try
            {
                _hubService.AssignTask(model);
                _toastNotification.AddSuccessToastMessage("Tasks have been successfully assigned.");
            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage(e.Message);
            }

            return RedirectToAction("Index", "Hub", new { title = model.HubName });
        }

        [HttpGet]
        public IActionResult UpdateTasks(UsersHubsViewModel hub)
        {
            return RedirectToAction("Index", "Hub");
        }
    }
}