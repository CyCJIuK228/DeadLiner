using DeadLinerWebApp.DAL.Entity;

namespace DeadLinerWebApp.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Hub> Hubs { get; }
        IRepository<UsersHubs> UsersHubs { get; }
        IRepository<RecoveryCode> Codes { get; }
        IRepository<UsersTasks> UsersTasks { get; }
        IRepository<Role> Roles { get; }

        void Save();
    }
}