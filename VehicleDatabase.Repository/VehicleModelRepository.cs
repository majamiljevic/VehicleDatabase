using AutoMapper;
using PagedList;
using PagedList.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDatabase.Common.Infrastructure;
using VehicleDatabase.DAL;
using VehicleDatabase.Model.Common;
using VehicleDatabase.Repository.Common;

namespace VehicleDatabase.Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private IRepository Repository { get; set; }

        public VehicleModelRepository(IRepository repository)
        {
            this.Repository = repository;
        }

        public async Task<IPagedList<IVehicleModel>> GetModelAsync(IFiltering filtering, ISorting sorting, IPaging paging)
        {
            var models = Repository.GetQueryable<VehicleModelEntity>();

            if (filtering.MakeId != null && filtering.MakeId != Guid.Empty)
            {
                models = models.Where(m => m.MakeId == filtering.MakeId);
            }

            if (!String.IsNullOrEmpty(filtering.SearchString))
            {
                models = models.Where(s => s.Name.Contains(filtering.SearchString) || s.Abrv.Contains(filtering.SearchString));
            }

            switch (sorting.SortOrder)
            {
                case "name_desc":
                    models = models.OrderByDescending(m => m.Name);
                    break;
                case "abrv":
                    models = models.OrderBy(m => m.Abrv);
                    break;
                case "abrv_desc":
                    models = models.OrderByDescending(m => m.Abrv);
                    break;
                default:  // Name ascending 
                    models = models.OrderBy(m => m.Name);
                    break;
            }

            var paginatedModels = await models.ToPagedListAsync(paging.Page, paging.PageSize);
            return Mapper.Map<IPagedList<VehicleModelEntity>, IPagedList<IVehicleModel>>(paginatedModels);
        }

        //add
        public Task<int> AddModelAsync(IVehicleModel model)
        {
            if (model.Id == null || model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
            }

            return Repository.AddAsync(Mapper.Map<VehicleModelEntity>(model));
        }

        //edit
        public Task<int> EditModelAsync(IVehicleModel model)
        {
            return Repository.EditAsync(Mapper.Map<VehicleModelEntity>(model));
        }

        //delete
        public Task<int> DeleteModelAsync(Guid modelId)
        {
            return Repository.DeleteAsync<VehicleModelEntity>(modelId);
        }

        //findbyid
        public async Task<IVehicleModel> FindByIdAsync(Guid modelId)
        {
            VehicleModelEntity make = await Repository.FindByIdAsync<VehicleModelEntity>(modelId);
            return Mapper.Map<IVehicleModel>(make);
        }

        //get filtered makes
        public async Task<IEnumerable<IVehicleMake>> GetFilteredMakesAsync(IFiltering filtering)
        {
            var makes = Repository.GetQueryable<VehicleMakeEntity>();

            if (!String.IsNullOrEmpty(filtering.SearchString))
            {
                makes = makes.Where(s => s.Name.Contains(filtering.SearchString) || s.Abrv.Contains(filtering.SearchString));
            }

            return Mapper.Map<IEnumerable<IVehicleMake>>(makes.ToList());
        }

        //delete multiple records
        public Task<int> DeleteMultipleRecordsAsync(Guid[] ids)
        {
            return Repository.DeleteBatchAsync<VehicleModelEntity>(ids);
        }
    }
}