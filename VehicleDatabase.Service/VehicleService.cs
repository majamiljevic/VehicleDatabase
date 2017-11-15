using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VehicleDatabase.Service.DAL;
using PagedList;


namespace VehicleDatabase.Service
{
    public class VehicleService : IVehicleService
    {
        private VehicleDatabaseDBContext context;
        public VehicleService()
        {
            this.context = new VehicleDatabaseDBContext();
        }


        // dodavanje proizvođača
        public int AddMake(VehicleMake make)
        {
            if (make.Id == null || make.Id == Guid.Empty)
            {
                make.Id = Guid.NewGuid();
            }

            context.Make.Add(make);
            try
            {
                return context.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }


        public IPagedList<VehicleMake> GetMakes(string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            IQueryable<VehicleMake> makes = context.Make;

            if (!String.IsNullOrEmpty(searchString))
            {
                makes = makes.Where(s => s.Name.Contains(searchString) || s.Abrv.Contains(searchString));
            }

            switch (sortOrder)
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
            return makes.ToPagedList(pageNumber, pageSize);
        }

        public int GetMakesCount(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return context.Make.Where(s => s.Name.Contains(searchString) || s.Abrv.Contains(searchString)).Count();
            }
            return context.Make.Count();
        }

        public int DeleteMake(Guid manufacturerId)
        {
            VehicleMake make = context.Make.FirstOrDefault(m => m.Id == manufacturerId);
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

        public int EditMake(VehicleMake make)
        {
            context.Entry(make).State = EntityState.Modified;
            try
            {
                return context.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }

        public VehicleMake FindMakeById(Guid manufacturerId)
        {
            return context.Make.FirstOrDefault(m => m.Id == manufacturerId);
        }



        //modeli

        public int AddModel(VehicleModel model)
        {
            context.Model.Add(model);
            try
            {
                return context.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }

        public IEnumerable<VehicleMake> GetAllMakes()
        {
            return context.Make.OrderBy(m => m.Name).ToList();
        }

        //dohvaćanje modela
        public IPagedList<VehicleModel> GetModels(string searchString, string sortOrder, Guid? MakeId, int pageNumber, int pageSize)
        {
            IQueryable<VehicleModel> models = context.Model;

            if (MakeId != null && MakeId != Guid.Empty)
            {
                models = models.Where(m => m.MakeId == MakeId);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                models = models.Where(s => s.Name.Contains(searchString) || s.Abrv.Contains(searchString));
            }

            switch (sortOrder)
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
            return models.ToPagedList(pageNumber, pageSize);
        }

        public int GetModelsCount(string searchString, Guid? MakeId)
        {
            IQueryable<VehicleModel> models = context.Model;

            if (MakeId != null && MakeId != Guid.Empty)
            {
                models = models.Where(m => m.MakeId == MakeId);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                return models.Where(s => s.Name.Contains(searchString) || s.Abrv.Contains(searchString)).Count();
            }
            return models.Count();
        }

        //update
        public int EditModel(VehicleModel model)
        {
            context.Entry(model).State = EntityState.Modified;
            try
            {
                return context.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }

        public VehicleModel FindModelById(Guid vehicleModelId)
        {
            return context.Model.FirstOrDefault(m => m.Id == vehicleModelId);
        }

        //delete
        public int DeleteModel(Guid vehicleModelId)
        {
            VehicleModel model = context.Model.FirstOrDefault(m => m.Id == vehicleModelId);

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