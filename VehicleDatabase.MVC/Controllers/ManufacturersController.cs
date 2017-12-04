using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleDatabase.MVC.Models;
using VehicleDatabase.Service;
using VehicleDatabase.Service.Infrastructure;
using PagedList;
using AutoMapper;


namespace VehicleDatabase.MVC.Controllers
{
    public class ManufacturersController : Controller
    {
        private IVehicleMakeService service;

        public ManufacturersController()
        {
            this.service = new VehicleMakeService();
        }

        public ActionResult Manufacturers(string SearchString, string SortOrder, int? Page)
        {
            ViewBag.CurrentSort = SortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            ViewBag.AbrvSortParm = SortOrder == "abrv" ? "abrv_desc" : "abrv";
            ViewBag.SearchString = SearchString;

            var filtering = new Filtering() { SearchString = SearchString };
            var sorting = new Sorting() { SortOrder = SortOrder };
            var paging = new Paging();
            if (Page != null)
            {
                paging.Page = (int)Page;
            }

            var makes = this.service.GetMakes(filtering, sorting, paging);
            var transformedMakes = Mapper.Map<IEnumerable<IVehicleMake>, IEnumerable<VehicleMakeViewModel>>(makes);

            return View(new StaticPagedList<VehicleMakeViewModel>(transformedMakes, makes.GetMetaData()));
        }

        //dodavanje proizvođača
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddManufacturer(VehicleMakeViewModel make)
        {
            if (ModelState.IsValid)
            {
                var transformedMake = Mapper.Map<VehicleMakeViewModel, VehicleMake>(make);
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
                var transformedMake = Mapper.Map<VehicleMakeViewModel, VehicleMake>(make);
                if (make.Id != null || make.Id != Guid.Empty)
                {
                    var result = this.service.EditMake(transformedMake);
                    if (result == 0)
                    {
                        ModelState.AddModelError("ValidationMessage", "Unable to edit record!");
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
                return PartialView("_ErrorModal", "Unable to edit record!");
            }

            var transformedModel = Mapper.Map<IVehicleMake, VehicleMakeViewModel>(model);
            return PartialView("_AddManufacturerModal", transformedModel);
        }
    }
}