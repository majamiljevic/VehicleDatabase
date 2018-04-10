using PagedList;

namespace VehicleDatabase.WebAPI.Models
{
    public class VehicleModelPagingModel
    {
        public int Page { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
        public IPagedList<VehicleModelModel> Models { get; set; }
    }
}