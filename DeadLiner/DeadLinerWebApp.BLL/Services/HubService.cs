using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.DAL.Entity;
using DeadLinerWebApp.DAL.Interfaces;
using DeadLinerWebApp.PL;
using DeadLinerWebApp.PL.Models;

namespace DeadLinerWebApp.BLL.Services
{
    public class HubService : IHubService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HubService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public HubsViewModel GetHubs(string userName)
        {
            var hubs = _unitOfWork.UsersHubs
                .GetWithInclude(p => p.User.FullName.Equals(userName), i => i.Role, i => i.Hub);
            var hubsList = _mapper.Map<List<HubModel>>(hubs.ToList());
            var user = _unitOfWork.Users.GetWithInclude(p => p.FullName.Equals(userName)).First();
            var invites = _unitOfWork.Invites.GetWithInclude(p => p.User.UserId == user.UserId && p.Status == "new",
                i => i.User, i => i.Hub).Select(s => s.Hub);

            var hubsInvitedList = _mapper.Map<List<HubModel>>(invites.ToList());
            return new HubsViewModel {Hubs = hubsList, HubsInvited = hubsInvitedList};
        }

        public UsersHubsViewModel GetTasks(string title, string userName)
        {
            var tasks = _unitOfWork.Hubs
                .GetWithInclude(p => p.Name.Equals(title), i => i.Tasks)
                .SelectMany(s => s.Tasks);

            if (tasks == null)
                throw new Exception("Hub doesn't exist.");

            var response = new UsersHubsViewModel
            {
                Assignments = new List<Assignment>()
            };

            foreach (var task in tasks)
            {
                var result = _unitOfWork.UsersTasks.GetWithInclude(p => p.TaskId == task.TaskId, i => i.User).ToList();
                result.ForEach(u => response.Assignments.Add(new Assignment
                {
                    Description = task.Description,
                    References = task.Resources,
                    Status = _mapper.Map<AssignmentStatus>(u.TaskStatus),
                    Title = task.Name,
                    UserName = u.User.FullName
                }));
            }

            var roles = _unitOfWork.UsersHubs.GetWithInclude(p => p.Hub.Name.Equals(title), i => i.Role, i => i.User)
                .ToList();
            response.Role = roles.Where(s => s.User.FullName.Equals(userName)).Select(i => i.Role).First().Title;
            response.MentorName = roles.Where(s => s.Role.Title.Equals("mentor")).Select(i => i.User).FirstOrDefault()
                ?.FullName;
            return response;
        }

        public void CreateHub(string title, string description, string userName)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var code = new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            _unitOfWork.Hubs.Create(new Hub
            {
                Description = description,
                Name = title,
                Code = code
            });

            _unitOfWork.Save();
            var user = _unitOfWork.Users.Find(p => p.FullName.Equals(userName)).First();
            var mentorRole = _unitOfWork.Roles.Find(r => r.Title.Equals("mentor")).First();
            var hub = _unitOfWork.Hubs.Find(s => s.Name == title).FirstOrDefault();

            _unitOfWork.UsersHubs.Create(new UsersHubs
            {
                HubId = hub.HubId,
                RoleId = mentorRole.RoleId,
                TeamName = "test",
                UserId = user.UserId
            });

            _unitOfWork.Save();
        }

        public void DeleteHub(string title)
        {
            var hub = _unitOfWork.Hubs.Find(p => p.Name.Equals(title)).First();
            _unitOfWork.Hubs.Delete(hub.HubId);
            _unitOfWork.Save();
        }

        public List<string> GetUsersInHub(string title)
        {
            var hub = _unitOfWork.Hubs.GetWithInclude(p => p.Name.Equals(title), i => i.Users).First();
            var users = hub.Users.Select(u => u.FullName).ToList();
            return users;
        }

        public void AssignTask(TaskViewModel model)
        {
            var hub = _unitOfWork.Hubs.GetWithInclude(p => p.Name.Equals(model.HubName)).First();
            _unitOfWork.Tasks.Create(new Task
            {
                Description = model.Description,
                HubId = hub.HubId,
                Name = model.TaskName,
                Resources = model.References
            });

            _unitOfWork.Save();
            var taskId = _unitOfWork.Tasks.Find(p => p.Name.Equals(model.TaskName)).First().TaskId;
            foreach (var user in model.AssignedTo)
            {
                var userId = _unitOfWork.Users.Find(p => p.FullName.Equals(user)).First().UserId;
                _unitOfWork.UsersTasks.Create(new UsersTasks
                {
                    TaskId = taskId,
                    TaskStatus = TaskStatus.New,
                    UserId = userId
                });
            }

            _unitOfWork.Save();
        }

        public List<string> GeInvitesInHub(string title)
        {
            var hub = _unitOfWork.Hubs.GetWithInclude(p => p.Name.Equals(title)).First().HubId;
            var invites = _unitOfWork.Invites.GetWithInclude(p => p.HubId == hub && p.Status == "new", i => i.User)
                .ToList();
            return invites.Select(s => s.User.FullName).ToList();
        }

        public string FindUser(string userName)
        {
            return _unitOfWork.Users.Find(p => p.FullName.Equals(userName)).FirstOrDefault()?.FullName;
        }

        public void InviteUser(InviteUserViewModel model)
        {
            var hub = _unitOfWork.Hubs.GetWithInclude(p => p.Name.Equals(model.Title)).First().HubId;
            var user = _unitOfWork.Users.GetWithInclude(p => p.FullName.Equals(model.UserName)).FirstOrDefault()?.UserId;
            if (user != null)
            {
                _unitOfWork.Invites.Create(new Invite { HubId = hub, UserId = user.Value, Status = "new" });
                _unitOfWork.Save();
            }
        }

        public void AcceptJoinToHub(string name, string title)
        {
            var invite = _unitOfWork.Invites.GetWithInclude(
                p => p.User.FullName.Equals(name) && p.Hub.Name.Equals(title) && p.Status == "new", i => i.User,
                i => i.Hub).First();
            invite.Status = "approved";
            _unitOfWork.Invites.Update(invite);

            var user = _unitOfWork.Users.Find(p => p.FullName.Equals(name)).First().UserId;
            var hub = _unitOfWork.Hubs.Find(p => p.Name.Equals(title)).First().HubId;
            var role = _unitOfWork.Roles.Find(p => p.Title == "mentee").First().RoleId;
            var userHub = new UsersHubs()
            {
                HubId = hub,
                UserId = user,
                TeamName = "",
                RoleId = role
            };

            _unitOfWork.UsersHubs.Create(userHub);
            _unitOfWork.Save();
        }

        public void RejectJoinToHub(string name, string title)
        {
            var invite = _unitOfWork.Invites.GetWithInclude(
                p => p.User.FullName.Equals(name) && p.Hub.Name.Equals(title) && p.Status == "new", i => i.User,
                i => i.Hub).First();
            invite.Status = "rejected";
            _unitOfWork.Invites.Update(invite);
            _unitOfWork.Save();
        }

        public void UpdateTask(string title, string status)
        {
            var task = _unitOfWork.UsersTasks.GetWithInclude(p => p.Task.Name.Equals(title)).First();
            task.TaskStatus = Enum.Parse<TaskStatus>(status);
            _unitOfWork.UsersTasks.Update(task);
            _unitOfWork.Save();
        }
    }
}