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
        private VehicleService service;

        public ManufacturersController()
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<VehicleMake, VehicleMakeViewModel>(); cfg.CreateMap<VehicleMakeViewModel, VehicleMake>(); });
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
            var totalMakeCount = this.service.GetMakesCount(searchString);

            return View(new StaticPagedList<VehicleMakeViewModel>(transformedMakes, pageNumber, pageSize, totalMakeCount));
        }

        // dodavanje proizvođača
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddManufacturer(VehicleMakeViewModel make)
        {
            if (ModelState.IsValid)
            {
                var transformedMake = this.mapper.Map<VehicleMakeViewModel, VehicleMake>(make);
                var makeExisits = this.service.MakeExists(transformedMake);
                if (make.Id == null || make.Id == Guid.Empty)
                {
                    if (makeExisits)
                    {
                        ModelState.AddModelError("CustomError", "Manufacturer with that name/abbreviation already exists");
                        return PartialView("_AddManufacturerModal", make);
                    }
                    else
                    {
                        this.service.AddMake(transformedMake);
                    }
                }
                else
                {
                    this.service.EditMake(transformedMake);
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
            this.service.DeleteMake(manufacturerId);
            return RedirectToAction("Manufacturers");
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
            var transformedModel = this.mapper.Map<VehicleMake, VehicleMakeViewModel>(model);
            return PartialView("_AddManufacturerModal", transformedModel);
        }
    }
}