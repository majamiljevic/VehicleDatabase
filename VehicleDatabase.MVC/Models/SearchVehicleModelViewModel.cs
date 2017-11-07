using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleDatabase.MVC.Models
{
    public class SearchVehicleModelViewModel
    {
        public IPagedList<VehicleModelViewModel> Model { get; set; }
        public IEnumerable<VehicleMakeViewModel> AllMakes { get; set; }
        public Guid? MakeId { get; set; }
    }
}