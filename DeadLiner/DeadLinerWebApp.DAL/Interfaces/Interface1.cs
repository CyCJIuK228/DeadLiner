﻿using DeadLinerWebApp.DAL.Models;

namespace DeadLinerWebApp.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        void Save();
    }
}