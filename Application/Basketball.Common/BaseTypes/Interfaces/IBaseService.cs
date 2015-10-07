using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Basketball.Common.BaseTypes.Interfaces
{
    public interface IBaseService<TEntity>
        where TEntity : class
    {
        void Attach(TEntity entity);
        void Load(TEntity entity, Expression<Func<TEntity, object>> t);
        void Load(TEntity entity, string navigationProperty);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        /// <summary>
        /// Example usage:
        ///  Get(
        ///        filter: d => !SelectedDepartment.HasValue || d.DepartmentID == departmentID,
        ///        orderBy: q => q.OrderBy(d => d.CourseID),
        ///        includeProperties: "Department")
        /// ); 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        List<TEntity> GetTop(int numberOfResults, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity Get(int id);
        //IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        void Save(TEntity entity);
        int Commit();
    }
}
