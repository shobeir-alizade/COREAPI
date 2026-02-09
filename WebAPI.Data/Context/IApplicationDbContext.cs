
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebAPI.Data.Context
{
    public interface IApplcationDbContext
    {

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry Entry(object entity);
        List<T> RunSp<T>(string StoreName, List<DbParameter> ListParamert) where T : new();
    }

}
