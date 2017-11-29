using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VehicleDatabase.MVC.Models;
using VehicleDatabase.Service;
using VehicleDatabase.Service.Infrastructure;
using PagedList;
using AutoMapper;

namespace VehicleDatabase.MVC.Controllers
{
    public class ModelsController : Controller
    {
        private VehicleModelService service;

        public ModelsController()
        {
            this.service = new VehicleModelService();
        }


        // GET: Models
        public ActionResult Models(Filtering filtering, Sorting sorting, Paging paging)
        {            
            ViewBag.CurrentSort = sorting.SortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sorting.SortOrder) ? "name_desc" : "";
            ViewBag.AbrvSortParm = sorting.SortOrder == "abrv" ? "abrv_desc" : "abrv";
            ViewBag.SearchString = filtering.SearchString;
            ViewBag.ManufacturerId = filtering.MakeId;

            var models = this.service.GetModels(filtering, sorting, paging);
            var transformedModels = Mapper.Map<IEnumerable<VehicleModel>, IEnumerable<VehicleModelViewModel>>(models);

            var pagedModel = new StaticPagedList<VehicleModelViewModel>(transformedModels, models.GetMetaData());

            var searchModel = new SearchVehicleModelViewModel();

            searchModel.AllMakes = Mapper.Map<IEnumerable<IVehicleMake>, IEnumerable<VehicleMakeViewModel>>(this.service.GetAllMakes());
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
                var transformedVehicleModel = Mapper.Map<VehicleModelViewModel, VehicleModel>(vehicleModel);
                if (vehicleModel.Id == null || vehicleModel.Id == Guid.Empty)
                {
                    var result = this.service.AddModel(transformedVehicleModel);
                    if (result == 0)
                    {
                        ModelState.AddModelError("ValidationMessage", "Unable to save vehicle model!");
                    }
                }
            }
            vehicleModel.AllMakes = Mapper.Map<IEnumerable<IVehicleMake>, IEnumerable<VehicleMakeViewModel>>(this.service.GetAllMakes());
            return PartialView("_AddVehicleModelModal", vehicleModel);
        }

        //editiranje modela
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVehicleModel(VehicleModelViewModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                var transformedVehicleModel = Mapper.Map<VehicleModelViewModel, VehicleModel>(vehicleModel);
                if (vehicleModel.Id != null || vehicleModel.Id != Guid.Empty)
                {
                    var result = this.service.EditModel(transformedVehicleModel);
                    if (result == 0)
                    {
                        ModelState.AddModelError("ValidationMessage", "Unable to save changes!");
                    }
                }
            }
            vehicleModel.AllMakes = Mapper.Map<IEnumerable<IVehicleMake>, IEnumerable<VehicleMakeViewModel>>(this.service.GetAllMakes());
            return PartialView("_AddVehicleModelModal", vehicleModel);
        }

        public ActionResult AddVehicleModel()
        {
            var model = new VehicleModelViewModel();
            model.AllMakes = Mapper.Map<IEnumerable<IVehicleMake>, IEnumerable<VehicleMakeViewModel>>(this.service.GetAllMakes());
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
                return PartialView("_ErrorModal", "Unable to save changes!");
            }
            var transformedModel = Mapper.Map<IVehicleModel, VehicleModelViewModel>(model);
            transformedModel.AllMakes = Mapper.Map<IEnumerable<IVehicleMake>, IEnumerable<VehicleMakeViewModel>>(this.service.GetAllMakes());
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