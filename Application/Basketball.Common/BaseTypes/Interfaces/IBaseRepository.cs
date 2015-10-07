using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Basketball.Common.BaseTypes.Interfaces
{
    public interface IBaseRepository<TEntity>
     where TEntity : class
    {
        void Delete(object id);
        void Delete(TEntity entityToDelete);

        IQueryable<TEntity> GetTop(int numberOfResults, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, System.Linq.IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity Get(int id);
        //IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        int Commit();
        void InsertOrUpdate(TEntity entity);
        void Attach(TEntity entity);
        void Load(TEntity entity, Expression<Func<TEntity, object>> t);
        void Load(TEntity entity, string navigationProperty);
    }
}
