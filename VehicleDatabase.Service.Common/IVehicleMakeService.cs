using System;
using System.Threading.Tasks;
using PagedList;
using VehicleDatabase.Common.Infrastructure;
using VehicleDatabase.Model.Common;

namespace VehicleDatabase.Service.Common
{
    public interface IVehicleMakeService
    {
        Task<int> AddMakeAsync(IVehicleMake make);
        Task<int> DeleteMakeAsync(Guid manufacturerId);
        Task<int> DeleteMultipleRecordsAsync(Guid[] ids);
        Task<int> EditMakeAsync(IVehicleMake make);
        Task<IVehicleMake> FindMakeByIdAsync(Guid manufacturerId);
        Task<IPagedList<IVehicleMake>> GetMakesAsync(IFiltering filtering, ISorting sorting, IPaging paging);
    }
}