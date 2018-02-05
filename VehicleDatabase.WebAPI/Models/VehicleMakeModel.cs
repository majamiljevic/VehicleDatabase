using System;

namespace VehicleDatabase.WebAPI.Models
{
    public class VehicleMakeModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}