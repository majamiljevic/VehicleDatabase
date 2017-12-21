using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using VehicleDatabase.Model.Common;


namespace VehicleDatabase.Model
{
    public class VehicleMake : IVehicleMake
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

       // public virtual IEnumerable<VehicleModel> Model { get; set; }

    }
}