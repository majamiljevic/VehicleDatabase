using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VehicleDatabase.Service.DAL;
using VehicleDatabase.Service.Infrastructure;
using PagedList;
using AutoMapper;

namespace VehicleDatabase.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private VehicleDatabaseDBContext context;

        public VehicleModelService()
        {
            this.context = new VehicleDatabaseDBContext();
        }

        public int AddModel(IVehicleModel model)
        {
            var transformedModel = Mapper.Map<IVehicleModel, VehicleModelEntity>(model);            
            context.Model.Add(transformedModel);
            try
            {
                return context.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }

        public IEnumerable<IVehicleMake> GetAllMakes()
        {
            var allMakes = context.Make.OrderBy(m => m.Name).ToList();
            return Mapper.Map<IEnumerable<VehicleMakeEntity>, IEnumerable<VehicleMake>>(allMakes);
        }

        //dohvaćanje modela
        public IPagedList<VehicleModel> GetModels(IFiltering filtering, ISorting sorting, IPaging paging)
        {
            var models = GetFilteredModels(filtering);

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
            var transformedModels = Mapper.Map<IEnumerable<VehicleModelEntity>, IEnumerable<VehicleModel>>(models.ToList());
            return transformedModels.ToPagedList(paging.Page, paging.PageSize);
        }

        public int GetModelsCount(IFiltering filtering)
        {            
            return GetFilteredModels(filtering).Count();
        }

        private IEnumerable<VehicleModelEntity> GetFilteredModels(IFiltering filtering)
        {
            IQueryable<VehicleModelEntity> models = context.Model;

            if (filtering.MakeId != null && filtering.MakeId != Guid.Empty)
            {
                models = models.Where(m => m.MakeId == filtering.MakeId);
            }

            if (!String.IsNullOrEmpty(filtering.SearchString))
            {
                models = models.Where(s => s.Name.Contains(filtering.SearchString) || s.Abrv.Contains(filtering.SearchString));
            }

            return models;
        }

        //update
        public int EditModel(IVehicleModel model)
        {
            var transformedModel = Mapper.Map<IVehicleModel, VehicleModelEntity>(model);
            context.Entry(transformedModel).State = EntityState.Modified;
            try
            {
                return context.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }

        public IVehicleModel FindModelById(Guid vehicleModelId)
        {
            var model = context.Model.FirstOrDefault(m => m.Id == vehicleModelId);
            return Mapper.Map<VehicleModelEntity, VehicleModel>(model);
        }

        //delete
        public int DeleteModel(Guid vehicleModelId)
        {
            VehicleModelEntity model = context.Model.FirstOrDefault(m => m.Id == vehicleModelId);
            var transformedModel = Mapper.Map<VehicleModelEntity, VehicleModel>(model);

            try
            {
                context.Model.Remove(model);
                return context.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }
    }
}
