using System;
using System.Collections.Generic;
using System.Linq;
using Basketball.Common.BaseTypes.Interfaces;
using System.Linq.Expressions;

namespace Basketball.Common.BaseTypes
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class 
    {
        private readonly IBaseRepository<TEntity> repository;

        protected BaseService(IBaseRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public void Attach(TEntity entity)
        {
            repository.Attach(entity);
        }

        public void Load(TEntity entity, Expression<Func<TEntity, object>> t)
        {
            repository.Load(entity, t);
        }

        public void Load(TEntity entity, string navigationProperty)
        {
            repository.Load(entity, navigationProperty);
        }

        public void Delete(object id)
        {
            repository.Delete(id);
        }

        public void Delete(TEntity entityToDelete)
        {
            repository.Delete(entityToDelete);
        }

        public virtual List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return repository.Get(filter, orderBy, includeProperties).ToList();
        }

        public virtual List<TEntity> GetTop(int numberOfResults, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return repository.GetTop(numberOfResults, filter, orderBy, includeProperties).ToList();
        }

        public TEntity Get(int id)
        {
            return repository.Get(id);
        }

        //public IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        //{
        //    return repository.GetWithRawSql(query, parameters);
        //}

        public void Insert(TEntity entity)
        {
            repository.Insert(entity);
        }

        public void Update(TEntity entityToUpdate)
        {
            repository.Update(entityToUpdate);
        }

        public void Save(TEntity entity)
        {
            repository.InsertOrUpdate(entity);
        }

        public int Commit()
        {
            return repository.Commit();
        }
    }
}
