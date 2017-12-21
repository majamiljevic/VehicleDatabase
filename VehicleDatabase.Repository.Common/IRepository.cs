using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleDatabase.Repository.Common
{
    public interface IRepository
    {
        Task<int> AddAsync<T>(T entity) where T : class;
        Task<int> DeleteAsync<T>(T entity) where T : class;
        Task<int> DeleteAsync<T>(Guid Id) where T : class;
        Task<int> EditAsync<T>(T entity) where T : class;
        Task<T> FindByIdAsync<T>(Guid Id) where T : class;
        Task<List<T>> GetAsync<T>(T entity) where T : class;
        IQueryable<T> GetQueryable<T>() where T : class;
    }
}