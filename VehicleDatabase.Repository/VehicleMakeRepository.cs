using System;
using System.Linq;
using System.Threading.Tasks;
using VehicleDatabase.DAL;
using VehicleDatabase.Common.Infrastructure;
using AutoMapper;
using VehicleDatabase.Repository.Common;
using VehicleDatabase.Model.Common;
using PagedList;
using PagedList.EntityFramework;

namespace VehicleDatabase.Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private IRepository Repository { get; set; }

        public VehicleMakeRepository(IRepository repository)
        {
            this.Repository = repository;
        }

        public async Task<IPagedList<IVehicleMake>> GetMakeAsync(IFiltering filtering, ISorting sorting, IPaging paging)
        {
            var makes = Repository.GetQueryable<VehicleMakeEntity>();

            if (!String.IsNullOrEmpty(filtering.SearchString))
            {
                makes = makes.Where(s => s.Name.Contains(filtering.SearchString) || s.Abrv.Contains(filtering.SearchString));
            }

            switch (sorting.SortOrder)
            {
                case "name_desc":
                    makes = makes.OrderByDescending(m => m.Name);
                    break;
                case "abrv":
                    makes = makes.OrderBy(m => m.Abrv);
                    break;
                case "abrv_desc":
                    makes = makes.OrderByDescending(m => m.Abrv);
                    break;
                default:  // Name ascending 
                    makes = makes.OrderBy(m => m.Name);
                    break;
            }

            var paginatedMakes = await makes.ToPagedListAsync(paging.Page, paging.PageSize);
            return Mapper.Map<IPagedList<IVehicleMake>>(paginatedMakes);
        }

        //add
        public Task<int> AddMakeAsync(IVehicleMake make)
        {
            if (make.Id == null || make.Id == Guid.Empty)
            {
                make.Id = Guid.NewGuid();
            }

            return Repository.AddAsync(Mapper.Map<VehicleMakeEntity>(make));
        }

        //edit
        public Task<int> EditMakeAsync(IVehicleMake make)
        {
            return Repository.EditAsync(Mapper.Map<VehicleMakeEntity>(make));
        }

        //delete
        public Task<int> DeleteMakeAsync(Guid makeId)
        {
            return Repository.DeleteAsync<VehicleMakeEntity>(makeId);
        }

        //findbyid
        public async Task<IVehicleMake> FindByIdAsync(Guid makeId)
        {
            VehicleMakeEntity make = await Repository.FindByIdAsync<VehicleMakeEntity>(makeId);
            return Mapper.Map<IVehicleMake>(make);
        }

        //delete multiple records
        public Task<int> DeleteMultipleRecordsAsync(Guid[] ids)
        {
            return Repository.DeleteBatchAsync<VehicleMakeEntity>(ids);
        }
    }
}
