using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VehicleDatabase.Common.Infrastructure;
using VehicleDatabase.Service.Common;
using PagedList;
using AutoMapper;
using VehicleDatabase.WebAPI.Models;
using VehicleDatabase.Model.Common;

namespace VehicleDatabase.WebAPI.Controllers
{
    [RoutePrefix("api/manufacturers")]
    public class ManufacturersController : ApiController
    {
        private IVehicleMakeService Service { get; set; }

        public ManufacturersController(IVehicleMakeService service)
        {
            this.Service = service;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAsync(string SearchString = "", string SortOrder = "", int? Page = null)
        {
            var filtering = new Filtering() { SearchString = SearchString };
            var sorting = new Sorting() { SortOrder = SortOrder };
            var paging = new Paging();
            if (Page != null)
            {
                paging.Page = (int)Page;
            }

            var makes = await this.Service.GetMakesAsync(filtering, sorting, paging);
            var transformedMakes = Mapper.Map<IPagedList<VehicleMakeModel>>(makes);

            var pagingModel = new VehicleMakePagingModel();

            pagingModel.Page = transformedMakes.PageNumber;
            pagingModel.PageCount = transformedMakes.PageCount;
            pagingModel.TotalCount = transformedMakes.TotalItemCount;
            pagingModel.Makes = transformedMakes;


            return Request.CreateResponse(HttpStatusCode.OK, pagingModel);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> PostAsync(VehicleMakeModel make)
        {
            var transformedMake = Mapper.Map<IVehicleMake>(make);
            if (make.Id == null || make.Id == Guid.Empty)
            {
                var result = await this.Service.AddMakeAsync(transformedMake);
                if (result == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }

            return Request.CreateResponse(HttpStatusCode.Created, make);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<HttpResponseMessage> PutAsync([FromUri]Guid id, VehicleMakeModel make)
        {
            var transformedMake = Mapper.Map<IVehicleMake>(make);
            if (make.Id != null || make.Id != Guid.Empty)
            {
                var result = await this.Service.EditMakeAsync(transformedMake);
                if (result == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, make);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> DeleteAsync([FromUri]Guid id)
        {
            var result = await this.Service.DeleteMakeAsync(id);
            if (result == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> FindMakeByIdAsync([FromUri]Guid id)
        {
            var make = await this.Service.FindMakeByIdAsync(id);
            if (make == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            var transformedMake = Mapper.Map<VehicleMakeModel>(make);
            return Request.CreateResponse(HttpStatusCode.OK, transformedMake);

        }
    }
}
