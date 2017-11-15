using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleDatabase.MVC.Models;
using VehicleDatabase.Service;
using PagedList;
using AutoMapper;

namespace VehicleDatabase.MVC.Controllers
{
    public class ModelsController : Controller
    {
        private IMapper mapper;
        private IVehicleService service;

        public ModelsController()
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<VehicleModel, VehicleModelViewModel>(); cfg.CreateMap<VehicleModelViewModel, VehicleModel>(); cfg.CreateMap<VehicleMake, VehicleMakeViewModel>(); cfg.CreateMap<VehicleMakeViewModel, VehicleMake>(); });
            this.mapper = config.CreateMapper();

            this.service = new VehicleService();
        }


        // GET: Models
        public ActionResult Models(string searchString, string sortOrder, Guid? MakeId, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AbrvSortParm = sortOrder == "abrv" ? "abrv_desc" : "abrv";
            ViewBag.SearchString = searchString;
            ViewBag.ManufacturerId = MakeId;

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var models = this.service.GetModels(searchString, sortOrder, MakeId, pageNumber, pageSize);
            var transformedModels = this.mapper.Map<IEnumerable<VehicleModel>, IEnumerable<VehicleModelViewModel>>(models);

            var pagedModel = new StaticPagedList<VehicleModelViewModel>(transformedModels, models.GetMetaData());

            var searchModel = new SearchVehicleModelViewModel();

            searchModel.AllMakes = this.mapper.Map<IEnumerable<VehicleMake>, IEnumerable<VehicleMakeViewModel>>(this.service.GetAllMakes());
            searchModel.Model = pagedModel;

            return View(searchModel);
        }

        //dodavanje modela
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVehicleModel(VehicleModelViewModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                var transformedVehicleModel = this.mapper.Map<VehicleModelViewModel, VehicleModel>(vehicleModel);
                if (vehicleModel.Id == null || vehicleModel.Id == Guid.Empty)
                {
                    var result = this.service.AddModel(transformedVehicleModel);
                    if (result == 0)
                    {
                        ModelState.AddModelError("ValidationMessage", "Unable to save vehicle model!");
                    }
                }
            }
            vehicleModel.AllMakes = this.mapper.Map<IEnumerable<VehicleMake>, IEnumerable<VehicleMakeViewModel>>(this.service.GetAllMakes());
            return PartialView("_AddVehicleModelModal", vehicleModel);
        }

        //editiranje modela
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVehicleModel(VehicleModelViewModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                var transformedVehicleModel = this.mapper.Map<VehicleModelViewModel, VehicleModel>(vehicleModel);
                if (vehicleModel.Id != null || vehicleModel.Id != Guid.Empty)
                {
                    var result = this.service.EditModel(transformedVehicleModel);
                    if (result == 0)
                    {
                        ModelState.AddModelError("ValidationMessage", "Unable to save changes!");
                    }
                }
            }
            vehicleModel.AllMakes = this.mapper.Map<IEnumerable<VehicleMake>, IEnumerable<VehicleMakeViewModel>>(this.service.GetAllMakes());
            return PartialView("_AddVehicleModelModal", vehicleModel);
        }

        public ActionResult AddVehicleModel()
        {
            var model = new VehicleModelViewModel();
            model.AllMakes = this.mapper.Map<IEnumerable<VehicleMake>, IEnumerable<VehicleMakeViewModel>>(this.service.GetAllMakes());
            var makesCount = model.AllMakes.Count();
            if (makesCount == 0)
            {
                return PartialView("_ErrorModal", "Add at least one manufacturer before adding models!");
            }
            return PartialView("_AddVehicleModelModal", model);
        }

        //edit
        public ActionResult EditVehicleModel(Guid vehicleModelId)
        {
            var model = this.service.FindModelById(vehicleModelId);
            if (model == null)
            {
                return PartialView("_ErrorModal", "Something went wrong!");
            }
            var transformedModel = this.mapper.Map<VehicleModel, VehicleModelViewModel>(model);
            transformedModel.AllMakes = this.mapper.Map<IEnumerable<VehicleMake>, IEnumerable<VehicleMakeViewModel>>(this.service.GetAllMakes());
            return PartialView("_AddVehicleModelModal", transformedModel);
        }

        //brisanje
        public ActionResult DeleteVehicleModelConfirmed(Guid vehicleModelId)
        {
            var result = this.service.DeleteModel(vehicleModelId);
            if (result == 0)
            {
                return PartialView("_ErrorModal", "Unable to delete vehicle model!");
            }
            return PartialView("_DeleteVehicleModel");
        }

        public ActionResult DeleteVehicleModel(Guid vehicleModelId)
        {
            var model = new DeleteVehicleModelViewModel();
            model.Id = vehicleModelId;
            return PartialView("_DeleteVehicleModel", model);
        }
    }
}