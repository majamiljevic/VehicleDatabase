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
    public class VehicleMakeService : IVehicleMakeService
    {
        private VehicleDatabaseDBContext context;

        public VehicleMakeService()
        {
            this.context = new VehicleDatabaseDBContext();
        }


        // dodavanje proizvođača
        public int AddMake(IVehicleMake make)
        {
            if (make.Id == null || make.Id == Guid.Empty)
            {
                make.Id = Guid.NewGuid();
            }

            var transformedMake = Mapper.Map<IVehicleMake, VehicleMakeEntity>(make);
            context.Make.Add(transformedMake);
            try
            {
                return context.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }

        public IPagedList<IVehicleMake> GetMakes(IFiltering filtering, ISorting sorting, IPaging paging)
        {
            var makes = GetFilteredMakes(filtering);

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
            var transformedMakes = Mapper.Map<IEnumerable<VehicleMakeEntity>, IEnumerable<VehicleMake>>(makes);
            return transformedMakes.ToPagedList(paging.Page, paging.PageSize);
        }

        public int GetMakesCount(IFiltering filtering)
        {
            return GetFilteredMakes(filtering).Count();
        }

        private IEnumerable<VehicleMakeEntity> GetFilteredMakes (IFiltering filtering)
        {
            IQueryable<VehicleMakeEntity> makes = context.Make;

            if (!String.IsNullOrEmpty(filtering.SearchString))
            {
                makes = makes.Where(s => s.Name.Contains(filtering.SearchString) || s.Abrv.Contains(filtering.SearchString));
            }

            return makes;
        }

        public int DeleteMake(Guid manufacturerId)
        {
            VehicleMakeEntity make = context.Make.FirstOrDefault(m => m.Id == manufacturerId);
            var transformedMake = Mapper.Map<VehicleMakeEntity, VehicleMake>(make);

            try
            {
                context.Make.Remove(make);
                return context.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }

        public int EditMake(IVehicleMake make)
        {
            var transformedMake = Mapper.Map<IVehicleMake, VehicleMakeEntity>(make);
            context.Entry(transformedMake).State = EntityState.Modified;
            try
            {
                return context.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }

        public IVehicleMake FindMakeById(Guid manufacturerId)
        {
            var make = context.Make.FirstOrDefault(m => m.Id == manufacturerId);
            return Mapper.Map<VehicleMakeEntity, VehicleMake>(make);
        }
    }
}