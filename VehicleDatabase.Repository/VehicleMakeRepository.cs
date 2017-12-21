using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDatabase.DAL;
using VehicleDatabase.Common.Infrastructure;
using VehicleDatabase.Model;
using AutoMapper;
using System.Data.Entity;
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

        public virtual async Task<IPagedList<IVehicleMake>> GetMakeAsync(IFiltering filtering, ISorting sorting, IPaging paging)
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

            var transformedMake = Mapper.Map<IEnumerable<IVehicleMake>>(makes);
            return transformedMake.ToPagedList(paging.Page, paging.PageSize);

            // var transformedMake = Mapper.Map<IQueryable<IVehicleMake>>(makes);
            // return await transformedMake.ToPagedListAsync(paging.Page, paging.PageSize);
        }

        //add
        public virtual Task<int> AddMakeAsync(IVehicleMake make)
        {
            if (make.Id == null || make.Id == Guid.Empty)
            {
                make.Id = Guid.NewGuid();
            }

            return Repository.AddAsync(Mapper.Map<VehicleMakeEntity>(make));
        }

        //edit
        public virtual Task<int> EditMakeAsync(IVehicleMake make)
        {
            return Repository.EditAsync(Mapper.Map<VehicleMakeEntity>(make));
        }

        //delete
        public virtual Task<int> DeleteMakeAsync(Guid makeId)
        {
            return Repository.DeleteAsync<VehicleMakeEntity>(makeId);
        }

        //FindByIdAsync
        public virtual async Task<IVehicleMake> FindByIdAsync(Guid id)
        {
            VehicleMakeEntity make = await Repository.GetQueryable<VehicleMakeEntity>().FirstOrDefaultAsync(m => m.Id == id);
            return Mapper.Map<IVehicleMake>(make);
        }
    }
}
