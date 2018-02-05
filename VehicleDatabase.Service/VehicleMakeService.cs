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

        public Task<int> AddMakeAsync(IVehicleMake make)
        {
            return Repository.AddMakeAsync(make);
        }

        public Task<IPagedList<IVehicleMake>> GetMakesAsync(IFiltering filtering, ISorting sorting, IPaging paging)
        {
            return Repository.GetMakeAsync(filtering, sorting, paging);
        }

        public Task<int> DeleteMakeAsync(Guid manufacturerId)
        {
            return Repository.DeleteMakeAsync(manufacturerId);
        }

        public Task<int> EditMakeAsync(IVehicleMake make)
        {
            return Repository.EditMakeAsync(make);
        }
      
        public Task<IVehicleMake> FindMakeByIdAsync(Guid manufacturerId)
        {
            return Repository.FindByIdAsync(manufacturerId);
        }
    }
}