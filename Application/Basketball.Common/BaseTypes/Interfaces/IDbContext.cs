using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Basketball.Common.BaseTypes.Interfaces
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        bool MustEagerLoadClosestNavigationProperties(); // True for WCF, false for web
    }

}
