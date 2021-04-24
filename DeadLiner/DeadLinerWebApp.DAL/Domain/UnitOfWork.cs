using System;
using DeadLinerWebApp.DAL.Entity;
using DeadLinerWebApp.DAL.Interfaces;

namespace DeadLinerWebApp.DAL.Domain
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _dbContext;
        private Repository<User> _userRepository;
        private Repository<Hub> _hubRepository;
        private Repository<UsersHubs> _userHubsRepository;
        private Repository<RecoveryCode> _recoveryCodeRepository;
        private Repository<UsersTasks> _usersTasksRepository;
        private Repository<Role> _rolesRepository;
        private Repository<UserInfo> _userInfoRepository;

        public UnitOfWork(DataContext context)
        {
            _dbContext = context;
        }

        public IRepository<User> Users => _userRepository ??= new Repository<User>(_dbContext);
        public IRepository<Hub> Hubs => _hubRepository ??= new Repository<Hub>(_dbContext);
        public IRepository<UsersHubs> UsersHubs => _userHubsRepository ??= new Repository<UsersHubs>(_dbContext);
        public IRepository<RecoveryCode> Codes => _recoveryCodeRepository ??= new Repository<RecoveryCode>(_dbContext);
        public IRepository<UsersTasks> UsersTasks => _usersTasksRepository ??= new Repository<UsersTasks>(_dbContext);
        public IRepository<Role> Roles => _rolesRepository ??= new Repository<Role>(_dbContext);
        public IRepository<UserInfo> UserInfos => _userInfoRepository ??= new Repository<UserInfo>(_dbContext);

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        private bool _disposed;
        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _dbContext.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}