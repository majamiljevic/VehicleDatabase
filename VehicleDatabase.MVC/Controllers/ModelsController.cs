using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VehicleDatabase.MVC.Models;
using PagedList;
using AutoMapper;
using VehicleDatabase.Common.Infrastructure;
using VehicleDatabase.Model.Common;
using System.Threading.Tasks;
using VehicleDatabase.Service.Common;

namespace VehicleDatabase.MVC.Controllers
{
    public class ModelsController : Controller
    {
        private IVehicleModelService Service { get; set; }

        public ModelsController(IVehicleModelService service)
        {
            this.Service = service;
        }


        //get models
        public async Task<ActionResult> Models(string SearchString, string SortOrder, string MakeName, Guid? MakeId, int? Page)
        {            
            ViewBag.CurrentSort = SortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            ViewBag.AbrvSortParm = SortOrder == "abrv" ? "abrv_desc" : "abrv";
            ViewBag.SearchString = SearchString;
            ViewBag.ManufacturerId = MakeId;

            var filtering = new Filtering() { SearchString = SearchString, MakeId = MakeId };
            var sorting = new Sorting() { SortOrder = SortOrder };
            var paging = new Paging();
            if (Page != null)
            {
                paging.Page = (int)Page;
            }

            var models = await this.Service.GetModelsAsync(filtering, sorting, paging);
            var transformedModels = Mapper.Map<IPagedList<VehicleModelViewModel>>(models);

            var searchModel = new SearchVehicleModelViewModel();

            searchModel.Model = transformedModels;
            searchModel.MakeName = MakeName;

            return View(searchModel);
        }

        //add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddVehicleModelAsync(VehicleModelViewModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                var transformedVehicleModel = Mapper.Map<IVehicleModel>(vehicleModel);
                
                if (vehicleModel.Id == null || vehicleModel.Id == Guid.Empty)
                {
                    var result = await this.Service.AddModelAsync(transformedVehicleModel);
                    if (result == 0)
                    {
                        ModelState.AddModelError("ValidationMessage", "Unable to save vehicle model! Check if manufacturer exists and try again.");
                    }
                }
            }
            return PartialView("_AddVehicleModelModal", vehicleModel);
        }

        //edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditVehicleModelAsync(VehicleModelViewModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                var transformedVehicleModel = Mapper.Map<IVehicleModel>(vehicleModel);
                if (vehicleModel.Id != null || vehicleModel.Id != Guid.Empty)
                {
                    var result = await this.Service.EditModelAsync(transformedVehicleModel);
                    if (result == 0)
                    {
                        ModelState.AddModelError("ValidationMessage", "Unable to save changes!");
                    }
                }
            }
            return PartialView("_AddVehicleModelModal", vehicleModel);
        }

        public async  Task<ActionResult> AddVehicleModelAsync()
        {
            var model = new VehicleModelViewModel();
            return PartialView("_AddVehicleModelModal", model);
        }

        //edit
        public async Task<ActionResult> EditVehicleModelAsync(Guid vehicleModelId)
        {
            var model = await this.Service.FindModelByIdAsync(vehicleModelId);
            if (model == null)
            {
                return PartialView("_ErrorModal", "Unable to save changes!");
            }
            var transformedModel = Mapper.Map<VehicleModelViewModel>(model);
            return PartialView("_AddVehicleModelModal", transformedModel);
        }

        //delete
        public async Task<ActionResult> DeleteVehicleModelConfirmedAsync(Guid vehicleModelId)
        {
            var result = await this.Service.DeleteModelAsync(vehicleModelId);
            if (result == 0)
            {
                return PartialView("_ErrorModal", "Unable to delete vehicle model!");
            }
            return PartialView("_DeleteVehicleModel");
        }

        public async Task<ActionResult> DeleteVehicleModelAsync(Guid vehicleModelId)
        {
            var model = new DeleteVehicleModelViewModel();
            model.Id = vehicleModelId;
            return PartialView("_DeleteVehicleModel", model);
        }

        //get filtered makes
        [HttpGet]
        public async Task<ActionResult> GetFilteredMakesAsync(string query)
        {
            var filtering = new Filtering() { SearchString = query };
            var makes = await Service.GetFilteredMakesAsync(filtering);
            var transformedMakes = Mapper.Map<IEnumerable<VehicleMakeViewModel>>(makes);
            return Json(makes, JsonRequestBehavior.AllowGet);
        }
    }
}