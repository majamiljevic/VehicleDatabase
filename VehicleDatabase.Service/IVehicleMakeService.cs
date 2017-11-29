using System;
using PagedList;
using VehicleDatabase.Service.Infrastructure;

namespace VehicleDatabase.Service
{
    public interface IVehicleMakeService
    {
        int AddMake(IVehicleMake make);
        int DeleteMake(Guid manufacturerId);
        int EditMake(IVehicleMake make);
        IVehicleMake FindMakeById(Guid manufacturerId);
        IPagedList<IVehicleMake> GetMakes(IFiltering filtering, ISorting sorting, IPaging paging);
        int GetMakesCount(IFiltering filtering);
    }
}