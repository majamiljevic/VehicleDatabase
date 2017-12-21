using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleDatabase.Common.Infrastructure;
using VehicleDatabase.Model;
using VehicleDatabase.Model.Common;
using PagedList;
using VehicleDatabase.DAL;

namespace VehicleDatabase.Repository.Common
{
    public interface IVehicleMakeRepository
    {
        Task<int> AddMakeAsync(IVehicleMake make);
        Task<int> DeleteMakeAsync(Guid makeId);
        Task<int> EditMakeAsync(IVehicleMake make);
        Task<IPagedList<IVehicleMake>> GetMakeAsync(IFiltering filtering, ISorting sorting, IPaging paging);
        Task<IVehicleMake> FindByIdAsync(Guid id);
    }
}