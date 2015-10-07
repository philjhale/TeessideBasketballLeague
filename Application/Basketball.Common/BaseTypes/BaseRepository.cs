using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Reflection;

using Basketball.Common.BaseTypes.Interfaces;
using Basketball.Common.Domain;
using Basketball.Common.Extensions;

namespace Basketball.Common.BaseTypes
{
    //public class SortExpression<TEntity, TType>
    //{
    //    Expression<Func<TEntity, TType>> SortProperty;
    //}

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {
        internal IDbContext context;
        internal IDbSet<TEntity> dbSet;

        // TODO? Disposable

        public BaseRepository(IDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public int Commit()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void Attach(TEntity entity)
        {
            dbSet.Attach(entity);
        }

        public void Load(TEntity entity, Expression<Func<TEntity, object>> t)
        {
            //dbSet.Attach(entity);
            context.Entry(entity).Reference(t).Load();
        }

        public void Load(TEntity entity, string navigationProperty)
        {
            context.Entry(entity).Reference(navigationProperty).Load();
        }

        /*
         * Example usage:
         * Get(
                filter: d => !SelectedDepartment.HasValue || d.DepartmentID == departmentID,
                orderBy: q => q.OrderBy(d => d.CourseID),
                includeProperties: "Department"));
         */
        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            
            if(context.MustEagerLoadClosestNavigationProperties())
            {
                var navigationProperties = typeof(TEntity).GetNavigationProperties();
                foreach (var navigationProperty in navigationProperties)
                {
                    query = query.Include(navigationProperty);
                }
            }
            else
            {
                // Optional eager loading
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }  
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public virtual IQueryable<TEntity> GetTop(
            int numberOfResults,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            return Get(filter, orderBy, includeProperties).Take(numberOfResults);
        }

        public virtual TEntity Get(int id)
        {
            return this.Get(x => x.Id == id).SingleOrDefault(); // Can't use dbSet.Find(id) because need to leverage the eager loading in Get() overload
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            if(context.Entry(entityToUpdate).State == EntityState.Detached)
                dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        // As yet untested
        public virtual void InsertOrUpdate(TEntity entity)
        {
            if(entity.Id <= 0)
                Insert(entity);
            else
                Update(entity);
        }

        //public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        //{
        //    return dbSet.SqlQuery(query, parameters).ToList();
        //}
    }
}