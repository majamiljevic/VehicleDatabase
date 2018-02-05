using System;
using System.Threading.Tasks;
using PagedList;
using VehicleDatabase.Common.Infrastructure;
using VehicleDatabase.Model.Common;
using System.Collections.Generic;

namespace VehicleDatabase.Service.Common
{
    public interface IVehicleModelService
    {
        Task<int> AddModelAsync(IVehicleModel model);
        Task<int> DeleteModelAsync(Guid vehicleModelId);
        Task<int> EditModelAsync(IVehicleModel model);
        Task<IVehicleModel> FindModelByIdAsync(Guid vehicleModelId);
        Task<IEnumerable<IVehicleMake>> GetFilteredMakesAsync(IFiltering filtering);
        Task<IPagedList<IVehicleModel>> GetModelsAsync(IFiltering filtering, ISorting sorting, IPaging paging);
    }
}