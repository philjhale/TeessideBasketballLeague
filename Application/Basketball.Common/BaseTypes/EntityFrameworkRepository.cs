using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using Basketball.Common.Domain;

namespace Basketball.Common.BaseTypes
{
    public class SqlRepository<T> : IRepository<T>
                                where T : class, IEntity
    {
        DbContext context;

        public SqlRepository(string connectionString)
        {
            //_objectSet = context.CreateObjectSet<T>();
            context = new DbContext(connectionString);
        }

        public IQueryable<T> FindAll()
        {
            return _objectSet;
        }
        public IQueryable<T> FindWhere(
                               Expression<Func<T, bool>> predicate)
        {
            return _objectSet.Where(predicate);
        }
        public T FindById(int id)
        {
            return _objectSet.Single(o => o.Id == id);
        }
        public void Add(T newEntity)
        {
            _objectSet.Add(newEntity);
        }
        public void Remove(T entity)
        {
            _objectSet.Remove(entity);
        }
        protected DbSet<T> _objectSet;
    }
}
