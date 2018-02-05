using PagedList;

namespace VehicleDatabase.WebAPI.Models
{
    public class VehicleMakePagingModel
    {
        public int Page { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
        public IPagedList<VehicleMakeModel> Makes { get; set; }
    }
}