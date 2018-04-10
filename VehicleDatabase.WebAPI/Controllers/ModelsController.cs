using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VehicleDatabase.Common.Infrastructure;
using AutoMapper;
using PagedList;
using VehicleDatabase.WebAPI.Models;
using VehicleDatabase.Service.Common;
using VehicleDatabase.Model.Common;
using System.Collections.Generic;

namespace VehicleDatabase.WebAPI.Controllers
{
    [RoutePrefix("api/models")]
    public class ModelsController : ApiController
    {
        private IVehicleModelService Service { get; set; }

        public ModelsController(IVehicleModelService service)
        {
            this.Service = service;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAsync(string SearchString = "", string SortOrder = "", Guid? MakeId = null, int? Page = null)
        {
            var filtering = new Filtering() { SearchString = SearchString, MakeId = MakeId };
            var sorting = new Sorting() { SortOrder = SortOrder };
            var paging = new Paging();
            if (Page != null)
            {
                paging.Page = (int)Page;
            }

            var models = await this.Service.GetModelsAsync(filtering, sorting, paging);
            var transformedModels = Mapper.Map<IPagedList<VehicleModelModel>>(models);

            var pagingModel = new VehicleModelPagingModel();

            pagingModel.Page = transformedModels.PageNumber;
            pagingModel.PageCount = transformedModels.PageCount;
            pagingModel.TotalCount = transformedModels.TotalItemCount;
            pagingModel.Models = transformedModels;
          
            return Request.CreateResponse(HttpStatusCode.OK, pagingModel);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> PostAsync(VehicleModelUpdateModel model)
        {
            var transforemdModel = Mapper.Map<IVehicleModel>(model);
            if  (model.Id == null || model.Id == Guid.Empty)
            {
                var result = await this.Service.AddModelAsync(transforemdModel);
                if (result == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<HttpResponseMessage> PutAsync([FromUri]Guid id, VehicleModelUpdateModel model)
        {
            var transformedModel = Mapper.Map<IVehicleModel>(model);
            if (model.Id != null || model.Id != Guid.Empty)
            {
                var result = await this.Service.EditModelAsync(transformedModel);
                if (result == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> DeleteAsync([FromUri]Guid id)
        {
            var result = await this.Service.DeleteModelAsync(id);
            if (result == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> FindModelByIdAsync([FromUri]Guid id)
        {
            var model = await this.Service.FindModelByIdAsync(id);
            if (model == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            var transformedModel = Mapper.Map<VehicleModelModel>(model);
            return Request.CreateResponse(HttpStatusCode.OK, transformedModel);
        }

        [HttpDelete]
        [Route("batch")]
        public async Task<HttpResponseMessage> DeleteMultipleRecordsAsync([FromBody]Guid[] ids)
        {
            var result = await this.Service.DeleteMultipleRecordsAsync(ids);
            if (result == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
