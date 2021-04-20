using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.DAL.Entity;
using DeadLinerWebApp.DAL.Interfaces;
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
                .GetWithInclude(p => p.User.FullName.Equals(userName), i => i.User, i => i.Hub).Select(i => i.Hub);
            var hubsList = _mapper.Map<List<HubModel>>(hubs.ToList());
            return new HubsViewModel { Hubs = hubsList };
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
                    Status = _mapper.Map<AssignmentStatus>(u.TaskStatus),
                    Title = task.Name,
                    UserName = u.User.FullName
                }));
            }

            var roles = _unitOfWork.UsersHubs.GetWithInclude(p => p.Hub.Name.Equals(title), i => i.Role, i => i.User)
                .ToList();
            response.Role = roles.Where(s => s.User.FullName.Equals(userName)).Select(i => i.Role).First().Title;
            response.MentorName = roles.Where(s => s.Role.Title.Equals("mentor")).Select(i => i.Role).FirstOrDefault()?.Title;
            return response;
        }

        public void CreateHub(string title, string description, string userName)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var code = new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            var user = _unitOfWork.Users.Find(p => p.FullName.Equals(userName)).First();
            var mentorRole = _unitOfWork.Roles.Find(r => r.Title.Equals("mentor")).First();

            _unitOfWork.Hubs.Create(new Hub
            {
                Description = description,
                Name = title,
                Code = code,
                Users = new List<User> { user }
            });

            _unitOfWork.Save();
        }
    }
}