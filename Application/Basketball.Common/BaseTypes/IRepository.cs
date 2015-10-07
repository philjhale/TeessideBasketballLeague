using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using Basketball.Common.Domain;

namespace Basketball.Common.BaseTypes
{
    public interface IRepository<T>
                where T : class, IEntity
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindWhere(Expression<Func<T, bool>> predicate);
        T FindById(int id);
        void Add(T newEntity);
        void Remove(T entity);
    }
}