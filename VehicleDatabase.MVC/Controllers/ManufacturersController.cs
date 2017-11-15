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
    public class ManufacturersController : Controller
    {
        private IMapper mapper;
        private IVehicleService service;

        public ManufacturersController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VehicleMake, VehicleMakeViewModel>();
                cfg.CreateMap<VehicleMakeViewModel, VehicleMake>();
            });
            this.mapper = config.CreateMapper();

            this.service = new VehicleService();
        }

        public ActionResult Manufacturers(string searchString, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AbrvSortParm = sortOrder == "abrv" ? "abrv_desc" : "abrv";
            ViewBag.SearchString = searchString;

            

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var makes = this.service.GetMakes(searchString, sortOrder, pageNumber, pageSize);
            var transformedMakes = this.mapper.Map<IEnumerable<VehicleMake>, IEnumerable<VehicleMakeViewModel>>(makes);

            return View(new StaticPagedList<VehicleMakeViewModel>(transformedMakes, makes.GetMetaData()));
        }

        //dodavanje proizvođača
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddManufacturer(VehicleMakeViewModel make)
        {
            if (ModelState.IsValid)
            {
                var transformedMake = this.mapper.Map<VehicleMakeViewModel, VehicleMake>(make);
                if (make.Id == null || make.Id == Guid.Empty)
                {
                    var result = this.service.AddMake(transformedMake);
                    if (result == 0)
                    {
                        ModelState.AddModelError("ValidationMessage", "Unable to save manufacturer!");
                    }
                }
            }
            return PartialView("_AddManufacturerModal", make);
        }

        //editiranje proizvođača
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditManufacturer(VehicleMakeViewModel make)
        {
            if (ModelState.IsValid)
            {
                var transformedMake = this.mapper.Map<VehicleMakeViewModel, VehicleMake>(make);
                if (make.Id != null || make.Id != Guid.Empty)
                {
                    var result = this.service.EditMake(transformedMake);
                    if (result == 0)
                    {
                        ModelState.AddModelError("ValidationMessage", "Unable to save changes!");
                    }
                }
            }
            return PartialView("_AddManufacturerModal", make);
        }

        public ActionResult AddManufacturer()
        {
            return PartialView("_AddManufacturerModal");
        }

        //brisanje
        public ActionResult DeleteManufacturerConfirmed(Guid manufacturerId)
        {
            var result = this.service.DeleteMake(manufacturerId);
            if (result == 0)
            {
                return PartialView("_ErrorModal", "Unable to delete manufacturer!");
            }

            return PartialView("_DeleteManufacturer");
        }

        public ActionResult DeleteManufacturer(Guid manufacturerId)
        {
            var model = new DeleteManufacturerViewModel();
            model.Id = manufacturerId;
            return PartialView("_DeleteManufacturer", model);
        }

        //edit
        public ActionResult EditManufacturer(Guid manufacturerId)
        {
            var model = this.service.FindMakeById(manufacturerId);
            if (model == null)
            {
                return PartialView("_ErrorModal", "Unable to save changes!");
            }

            var transformedModel = this.mapper.Map<VehicleMake, VehicleMakeViewModel>(model);
            return PartialView("_AddManufacturerModal", transformedModel);
        }
    }
}