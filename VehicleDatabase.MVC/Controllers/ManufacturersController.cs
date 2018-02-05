using System;
using System.Web.Mvc;
using VehicleDatabase.MVC.Models;
using PagedList;
using AutoMapper;
using System.Threading.Tasks;
using VehicleDatabase.Common.Infrastructure;
using VehicleDatabase.Model.Common;
using VehicleDatabase.Service.Common;

namespace VehicleDatabase.MVC.Controllers
{
    public class ManufacturersController : Controller
    {
        private IVehicleMakeService Service { get; set; }

        public ManufacturersController(IVehicleMakeService service)
        {
            this.Service = service;
        }

        public async Task<ActionResult> Manufacturers(string SearchString, string SortOrder, int? Page)
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
           

            var makes =  await this.Service.GetMakesAsync(filtering, sorting, paging);
            var transformedMakes = Mapper.Map<IPagedList<VehicleMakeViewModel>>(makes);

            return View(transformedMakes);
        }

        //add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddManufacturer(VehicleMakeViewModel make)
        {
            if (ModelState.IsValid)
            {
                var transformedMake = Mapper.Map<IVehicleMake>(make);
                if (make.Id == null || make.Id == Guid.Empty)
                {
                    var result = await this.Service.AddMakeAsync(transformedMake);
                    if (result == 0)
                    {
                        ModelState.AddModelError("ValidationMessage", "Unable to save manufacturer!");
                    }
                }
            }
            return PartialView("_AddManufacturerModal", make);
        }

        //edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditManufacturer(VehicleMakeViewModel make)
        {
            if (ModelState.IsValid)
            {
                var transformedMake = Mapper.Map<IVehicleMake>(make);
                if (make.Id != null || make.Id != Guid.Empty)
                {
                    var result = await this.Service.EditMakeAsync(transformedMake);
                    if (result == 0)
                    {
                        ModelState.AddModelError("ValidationMessage", "Unable to save changes!");
                    }
                }
            }
            return PartialView("_AddManufacturerModal", make);
        }


        public async Task<ActionResult> AddManufacturer()
        {
            return PartialView("_AddManufacturerModal");
        }

        //delete
        public async Task<ActionResult> DeleteManufacturerConfirmed(Guid manufacturerId)
        {
            var result = await this.Service.DeleteMakeAsync(manufacturerId);
            if (result == 0)
            {
                return PartialView("_ErrorModal", "Unable to delete manufacturer!");
            }

            return PartialView("_DeleteManufacturer");
        }

        public async Task<ActionResult> DeleteManufacturer(Guid manufacturerId)
        {
            var model = new DeleteManufacturerViewModel();
            model.Id = manufacturerId;
            return PartialView("_DeleteManufacturer", model);
        }

        //edit
        public async Task<ActionResult> EditManufacturer(Guid manufacturerId)
        {
            var model = await this.Service.FindMakeByIdAsync(manufacturerId);
            if (model == null)
            {
                return PartialView("_ErrorModal", "Unable to save changes!");
            }

            var transformedModel = Mapper.Map<VehicleMakeViewModel>(model);
            return PartialView("_AddManufacturerModal", transformedModel);
        }
    }
}