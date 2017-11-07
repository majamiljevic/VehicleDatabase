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
        private VehicleService service;

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
            var totalModelCount = this.service.GetModelsCount(searchString, MakeId);

            var pagedModel = new StaticPagedList<VehicleModelViewModel>(transformedModels, pageNumber, pageSize, totalModelCount);

            var searchModel = new SearchVehicleModelViewModel();

            searchModel.AllMakes = this.mapper.Map<IEnumerable<VehicleMake>, IEnumerable<VehicleMakeViewModel>>(this.service.GetAllMakes());
            searchModel.Model = pagedModel;

            return View(searchModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVehicleModel(VehicleModelViewModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                var transformedVehicleModel = this.mapper.Map<VehicleModelViewModel, VehicleModel>(vehicleModel);
                if (vehicleModel.Id == null || vehicleModel.Id == Guid.Empty)
                {
                    this.service.AddModel(transformedVehicleModel);
                }
                else
                {
                    this.service.EditModel(transformedVehicleModel);
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
                return PartialView("_AddModelErrorModal");
            }
            return PartialView("_AddVehicleModelModal", model);
        }

        //edit
        public ActionResult EditVehicleModel(Guid vehicleModelId)
        {
            var model = this.service.FindModelById(vehicleModelId);
            var transformedModel = this.mapper.Map<VehicleModel, VehicleModelViewModel>(model);
            transformedModel.AllMakes = this.mapper.Map<IEnumerable<VehicleMake>, IEnumerable<VehicleMakeViewModel>>(this.service.GetAllMakes());
            return PartialView("_AddVehicleModelModal", transformedModel);
        }

        //brisanje
        public ActionResult DeleteVehicleModelConfirmed(Guid vehicleModelId)
        {
            this.service.DeleteModel(vehicleModelId);
            return RedirectToAction("Models");
        }

        public ActionResult DeleteVehicleModel(Guid vehicleModelId)
        {
            var model = new DeleteVehicleModelViewModel();
            model.Id = vehicleModelId;
            return PartialView("_DeleteVehicleModel", model);
        }
    }
}