using DeadLinerWebApp.DAL.Models;

namespace DeadLinerWebApp.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Hub> Hubs { get; }
        IRepository<UsersHubs> UsersHubs { get;  }
        void Save();
    }
}