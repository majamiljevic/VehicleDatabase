using System;
using VehicleDatabase.Common.Infrastructure;
using PagedList;
using System.Threading.Tasks;
using VehicleDatabase.Repository.Common;
using VehicleDatabase.Model.Common;
using VehicleDatabase.Service.Common;

namespace VehicleDatabase.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private IVehicleMakeRepository Repository { get; set; }

        public VehicleMakeService(IVehicleMakeRepository repository)
        {
            this.Repository = repository;
        }

        //add
        public Task<int> AddMakeAsync(IVehicleMake make)
        {
            return Repository.AddMakeAsync(make);
        }

        //getmakes
        public Task<IPagedList<IVehicleMake>> GetMakesAsync(IFiltering filtering, ISorting sorting, IPaging paging)
        {
            return Repository.GetMakeAsync(filtering, sorting, paging);
        }

        //delete
        public Task<int> DeleteMakeAsync(Guid manufacturerId)
        {
            return Repository.DeleteMakeAsync(manufacturerId);
        }

        //edit
        public Task<int> EditMakeAsync(IVehicleMake make)
        {
            return Repository.EditMakeAsync(make);
        }

        //find by id
        public Task<IVehicleMake> FindMakeByIdAsync(Guid manufacturerId)
        {
            return Repository.FindByIdAsync(manufacturerId);
        }

        //delete multiple records
        public Task<int> DeleteMultipleRecordsAsync(Guid[] ids)
        {
            return Repository.DeleteMultipleRecordsAsync(ids);
        }
    }
}