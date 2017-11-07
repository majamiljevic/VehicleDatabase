using System;
using System.Collections.Generic;

namespace VehicleDatabase.Service
{
    public interface IVehicleService
    {
        int AddMake(VehicleMake make);
        int AddModel(VehicleModel model);
        int DeleteMake(Guid manufacturerId);
        int DeleteModel(Guid vehicleModelId);
        int EditMake(VehicleMake make);
        int EditModel(VehicleModel model);
        VehicleMake FindMakeById(Guid manufacturerId);
        VehicleModel FindModelById(Guid vehicleModelId);
        IEnumerable<VehicleMake> GetAllMakes();
        IEnumerable<VehicleMake> GetMakes(string searchString, string sortOrder, int pageNumber, int pageSize);
        int GetMakesCount(string searchString);
        IEnumerable<VehicleModel> GetModels(string searchString, string sortOrder, Guid? MakeId, int pageNumber, int pageSize);
        int GetModelsCount(string searchString, Guid? MakeId);
        bool MakeExists(VehicleMake make);
    }
}