using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DeadLinerWebApp.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetWithInclude(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);
    }
}