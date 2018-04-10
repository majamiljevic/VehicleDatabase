using System;
using System.Collections.Generic;
using PagedList;
using VehicleDatabase.Model.Common;
using VehicleDatabase.Common.Infrastructure;
using System.Threading.Tasks;
using VehicleDatabase.Repository.Common;
using VehicleDatabase.Service.Common;

namespace VehicleDatabase.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private IVehicleModelRepository Repository { get; set; }

        public VehicleModelService(IVehicleModelRepository repository)
        {
            this.Repository = repository;
        }

        //getmodels
        public Task<IPagedList<IVehicleModel>> GetModelsAsync(IFiltering filtering, ISorting sorting, IPaging paging)
        {
            return Repository.GetModelAsync(filtering, sorting, paging);
        }

        //add
        public Task<int> AddModelAsync(IVehicleModel model)
        {
            return Repository.AddModelAsync(model);
        }

        //edit
        public Task<int> EditModelAsync(IVehicleModel model)
        {
            return Repository.EditModelAsync(model);
        }

        //find by id
        public Task<IVehicleModel> FindModelByIdAsync(Guid vehicleModelId)
        {
            return Repository.FindByIdAsync(vehicleModelId);
        }

        //delete
        public Task<int> DeleteModelAsync(Guid vehicleModelId)
        {
            return Repository.DeleteModelAsync(vehicleModelId);
        }

        //get filtered makes
        public Task<IEnumerable<IVehicleMake>> GetFilteredMakesAsync(IFiltering filtering)
        {
            return Repository.GetFilteredMakesAsync(filtering);
        }

        //delete multiple records
        public Task<int> DeleteMultipleRecordsAsync(Guid[] ids)
        {
            return Repository.DeleteMultipleRecordsAsync(ids);
        }
    }
}
