
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPI.Core;
using System.Data.Common;
using WebAPI.Data.Context;

namespace WebAPI.Data.Repository
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        #region Fields
        private readonly IApplcationDbContext _context ;

        private DbSet<TEntity> entities;

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (entities == null)
                    entities = _context.Set<TEntity>();

                return entities;
            }
        }

        #endregion
        public EFRepository(IApplcationDbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Table => Entities;


        public IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<TEntity>().Remove(entity);

            _context.SaveChanges();
        }

        public async virtual Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<TEntity>().Remove(entity);

            await _context.SaveChangesAsync();
        }


        public  virtual TEntity GetById(params object[] ids)
        {
            return  _context.Set<TEntity>().Find(ids);
        }
        public  virtual TEntity GetByIdAsNoTracking(params object[] ids)
        {
            var entity =  _context.Set<TEntity>().Find(ids);

            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public async virtual Task<TEntity> GetByIdAsync(params object[] ids)
        {
            return await _context.Set<TEntity>().FindAsync(ids);
        }
        public async virtual Task<TEntity> GetByIdAsNoTrackingAsync(params object[] ids)
        {
            var entity = await _context.Set<TEntity>().FindAsync(ids);

            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }
        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }


        public async virtual Task InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        public async virtual Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<TEntity>().Update(entity);
           await _context.SaveChangesAsync();
        }

        public List<T> RunFunctionDb<T>(string functionName,List<DbParameter> paramters) where T : new()
        {
            return _context.RunSp<T>(functionName, paramters);
        }
      
    }
}
