using System;
using System.Threading.Tasks;
using PagedList;
using VehicleDatabase.Common.Infrastructure;
using VehicleDatabase.Model.Common;

namespace VehicleDatabase.Repository.Common
{
    public interface IVehicleMakeRepository
    {
        Task<int> AddMakeAsync(IVehicleMake make);
        Task<int> DeleteMakeAsync(Guid makeId);
        Task<int> DeleteMultipleRecordsAsync(Guid[] ids);
        Task<int> EditMakeAsync(IVehicleMake make);
        Task<IVehicleMake> FindByIdAsync(Guid makeId);
        Task<IPagedList<IVehicleMake>> GetMakeAsync(IFiltering filtering, ISorting sorting, IPaging paging);
    }
}