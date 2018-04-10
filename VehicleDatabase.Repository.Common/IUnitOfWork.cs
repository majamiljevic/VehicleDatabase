using System;
using System.Threading.Tasks;

namespace VehicleDatabase.Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> AddAsync<T>(T entity) where T : class;
        Task<int> CommitAsync();
        Task<int> DeleteAsync<T>(T entity) where T : class;
        Task<int> DeleteAsync<T>(Guid Id) where T : class;
        Task<int> UpdateAsync<T>(T entity) where T : class;
    }
}