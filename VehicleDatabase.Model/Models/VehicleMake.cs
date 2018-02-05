using System;
using System.Collections.Generic;
using VehicleDatabase.Model.Common;

namespace VehicleDatabase.Model
{
    public class VehicleMake : IVehicleMake
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual IEnumerable<IVehicleModel> Model { get; set; }
    }
}