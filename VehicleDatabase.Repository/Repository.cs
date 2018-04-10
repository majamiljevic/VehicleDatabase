using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VehicleDatabase.DAL;
using VehicleDatabase.Repository.Common;

namespace VehicleDatabase.Repository
{
    public class Repository : IRepository
    {
        private VehicleDatabaseDBContext DbContext { get; set; }
        private IUnitOfWorkFactory UnitOfWorkFactory { get; set; }

        public Repository(VehicleDatabaseDBContext dbContext, IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (dbContext == null)
            {
                throw new ArgumentException("DbContext");
            }
            DbContext = dbContext;
            UnitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<List<T>> GetAsync<T>(T entity) where T : class
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public async Task<int> AddAsync<T>(T entity) where T : class
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                await uow.AddAsync(entity);
                return await uow.CommitAsync();
            }
        }

        public async Task<int> EditAsync<T>(T entity) where T : class
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                await uow.UpdateAsync(entity);
                return await uow.CommitAsync();
            }
        }

        public async Task<T> FindByIdAsync<T>(Guid Id) where T : class
        {
            return await DbContext.Set<T>().FindAsync(Id);
        }

        public async Task<int> DeleteAsync<T>(T entity) where T : class
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                await uow.DeleteAsync(entity);
                return await uow.CommitAsync();
            }
        }

        public async Task<int> DeleteAsync<T>(Guid Id) where T : class
        {
            var entity = await FindByIdAsync<T>(Id);
            
            if (entity == null)
            {
                throw new KeyNotFoundException("Not found");
            }
            return await DeleteAsync<T>(entity);
        }

        public IQueryable<T> GetQueryable<T>() where T : class
        {
            return DbContext.Set<T>();
        }

        public async Task<int> DeleteBatchAsync<T>(Guid[] ids) where T : class
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                foreach (Guid id in ids)
                {
                    await uow.DeleteAsync<T>(id);
                }

                return await uow.CommitAsync();
            }
        }
    }
}

