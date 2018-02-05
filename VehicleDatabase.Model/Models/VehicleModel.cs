using System;
using VehicleDatabase.Model.Common;

namespace VehicleDatabase.Model
{
    public class VehicleModel : IVehicleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public Guid MakeId { get; set; }
        public virtual IVehicleMake Make { get; set; }
    }
}