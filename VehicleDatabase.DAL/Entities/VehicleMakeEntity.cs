using System;
using System.Collections.Generic;

namespace VehicleDatabase.DAL
{
    public class VehicleMakeEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual IEnumerable<VehicleModelEntity> Model { get; set; }
        
    }
}