using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using VehicleDatabase.DAL;
using VehicleDatabase.Repository.Common;

namespace VehicleDatabase.Repository
{
    public class Repository : IRepository 
    {
        private VehicleDatabaseDBContext DbContext { get; set; }

        public Repository(VehicleDatabaseDBContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentException("DbContext");
            }
            DbContext = dbContext;
        }

        public virtual async Task<List<T>> GetAsync<T>(T entity) where T : class
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<int> AddAsync<T>(T entity) where T : class
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbContext.Set<T>().Add(entity);
            }
            try
            {
                return await DbContext.SaveChangesAsync();
            }
            catch
            {
                return 0;
            }
        }

        public virtual async Task<int> EditAsync<T>(T entity) where T : class
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbContext.Set<T>().Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
            try
            {
                return await DbContext.SaveChangesAsync();
            }
            catch
            {
                return 0;
            }
        }

        public virtual Task<T> FindByIdAsync<T>(Guid Id) where T : class
        {
            return DbContext.Set<T>().FindAsync(Id);
        }

        public virtual async Task<int> DeleteAsync<T>(T entity) where T : class
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbContext.Set<T>().Attach(entity);
                DbContext.Set<T>().Remove(entity);
            }
            try
            {
                return await DbContext.SaveChangesAsync();
            }
            catch
            {
                return 0;
            }
        }


        public virtual async Task<int> DeleteAsync<T>(Guid Id) where T : class
        {
            var entity = await FindByIdAsync<T>(Id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Not found");
            }
            return await DeleteAsync<T>(entity);
        }

        public virtual IQueryable<T> GetQueryable<T>() where T : class
        {
            return DbContext.Set<T>();
        }
    }

}

