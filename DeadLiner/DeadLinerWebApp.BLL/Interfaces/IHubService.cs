using System.Collections.Generic;
using DeadLinerWebApp.PL;
using DeadLinerWebApp.PL.Models;

namespace DeadLinerWebApp.BLL.Interfaces
{
    public interface IHubService
    {
        HubsViewModel GetHubs(string userName);
        UsersHubsViewModel GetTasks(string title, string userName);
        void CreateHub(string title, string description, string userName);
        void DeleteHub(string title);
        List<string> GetUsersInHub(string title);
        void AssignTask(TaskViewModel model);
        List<string> GeInvitesInHub(string title);
        string FindUser(string userName);
        void InviteUser(InviteUserViewModel model);
        void AcceptJoinToHub(string name, string title);
        void RejectJoinToHub(string name, string title);
        void UpdateTask(string title, string status);
    }
}