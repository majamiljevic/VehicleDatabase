using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PagedList;
using VehicleDatabase.Common.Infrastructure;
using VehicleDatabase.Model.Common;

namespace VehicleDatabase.Repository.Common
{
    public interface IVehicleModelRepository
    {
        Task<int> AddModelAsync(IVehicleModel model);
        Task<int> DeleteModelAsync(Guid modelId);
        Task<int> DeleteMultipleRecordsAsync(Guid[] ids);
        Task<int> EditModelAsync(IVehicleModel model);
        Task<IVehicleModel> FindByIdAsync(Guid modelId);
        Task<IEnumerable<IVehicleMake>> GetFilteredMakesAsync(IFiltering filtering);
        Task<IPagedList<IVehicleModel>> GetModelAsync(IFiltering filtering, ISorting sorting, IPaging paging);
    }
}