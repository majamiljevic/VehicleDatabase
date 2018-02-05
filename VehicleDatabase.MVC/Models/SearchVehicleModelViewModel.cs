using PagedList;
using System;
using System.Collections.Generic;

namespace VehicleDatabase.MVC.Models
{
    public class SearchVehicleModelViewModel
    {
        public IPagedList<VehicleModelViewModel> Model { get; set; }
        public IEnumerable<VehicleMakeViewModel> AllMakes { get; set; }
        public Guid? MakeId { get; set; }
        public string MakeName { get; set; }
    }
}