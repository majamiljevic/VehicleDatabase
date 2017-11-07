using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleDatabase.Service.DAL;


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
            return context.SaveChanges();
        }


        public IEnumerable<VehicleMake> GetMakes(string searchString, string sortOrder, int pageNumber, int pageSize)
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
            return makes.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
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
            context.Make.Remove(make);
            return context.SaveChanges();
        }

        public int EditMake(VehicleMake make)
        {
            var makeToUpdate = context.Make.FirstOrDefault(m => m.Id == make.Id);
            makeToUpdate.Name = make.Name;
            makeToUpdate.Abrv = make.Abrv;
            return context.SaveChanges();
        }

        public VehicleMake FindMakeById(Guid manufacturerId)
        {
            return context.Make.FirstOrDefault(m => m.Id == manufacturerId);
        }

        public bool MakeExists(VehicleMake make)
        {
            return context.Make.Any(ma => ma.Name.Equals(make.Name) || ma.Abrv.Equals(make.Abrv));
        }




        //modeli

        public int AddModel(VehicleModel model)
        {
            context.Model.Add(model);
            return context.SaveChanges();
        }

        public IEnumerable<VehicleMake> GetAllMakes()
        {
            return context.Make.ToList();
        }

        //dohvaćanje modela
        public IEnumerable<VehicleModel> GetModels(string searchString, string sortOrder, Guid? MakeId, int pageNumber, int pageSize)
        {
            IQueryable<VehicleModel> models = context.Model;

            if (MakeId != null && MakeId != Guid.Empty)
            {
                models = context.Model.Where(m => m.MakeId == MakeId);
                if (!String.IsNullOrEmpty(searchString))
                {
                    models = models.Where(s => s.Name.Contains(searchString) || s.Abrv.Contains(searchString));
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    models = models.Where(s => s.Name.Contains(searchString) || s.Abrv.Contains(searchString));
                }
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
            return models.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }

        public int GetModelsCount(string searchString, Guid? MakeId)
        {
            IQueryable<VehicleModel> models = context.Model;
            if (MakeId != null && MakeId != Guid.Empty)
            {
                models = context.Model.Where(m => m.MakeId == MakeId);
                if (!String.IsNullOrEmpty(searchString))
                {
                    return models.Where(s => s.Name.Contains(searchString) || s.Abrv.Contains(searchString)).Count();
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    return models.Where(s => s.Name.Contains(searchString) || s.Abrv.Contains(searchString)).Count();
                }
            }
            return models.Count();
        }

        //update
        public int EditModel(VehicleModel model)
        {
            var modelToUpdate = context.Model.FirstOrDefault(m => m.Id == model.Id);
            modelToUpdate.MakeId = model.MakeId;
            modelToUpdate.Name = model.Name;
            modelToUpdate.Abrv = model.Abrv;
            return context.SaveChanges();
        }

        public VehicleModel FindModelById(Guid vehicleModelId)
        {
            return context.Model.FirstOrDefault(m => m.Id == vehicleModelId);
        }

        //delete
        public int DeleteModel(Guid vehicleModelId)
        {
            VehicleModel model = context.Model.FirstOrDefault(m => m.Id == vehicleModelId);
            context.Model.Remove(model);
            return context.SaveChanges();
        }
    }
}