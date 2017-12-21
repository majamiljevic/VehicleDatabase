/*using System;
using System.Collections.Generic;
using PagedList;
using VehicleDatabase.Service.Infrastructure;

namespace VehicleDatabase.Service
{
    public interface IVehicleModelService
    {
        int AddModel(IVehicleModel model);
        int DeleteModel(Guid vehicleModelId);
        int EditModel(IVehicleModel model);
        IVehicleModel FindModelById(Guid vehicleModelId);
        IEnumerable<IVehicleMake> GetAllMakes();
        IPagedList<VehicleModel> GetModels(IFiltering filtering, ISorting sorting, IPaging paging);
        int GetModelsCount(IFiltering filtering);
    }
}*/