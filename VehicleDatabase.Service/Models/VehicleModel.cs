using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleDatabase.Service
{
    public class VehicleModel : IVehicleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public Guid MakeId { get; set; }
        public virtual VehicleMake Make { get; set; }
    }
}