using DeadLinerWebApp.PL.Models;

namespace DeadLinerWebApp.BLL.Interfaces
{
    public interface IHubService
    {
        HubsViewModel GetHubs(string userName);
        UsersHubsViewModel GetTasks(string title, string userName);
        void CreateHub(string title, string description, string userName);
        void DeleteHub(string title);
    }
}