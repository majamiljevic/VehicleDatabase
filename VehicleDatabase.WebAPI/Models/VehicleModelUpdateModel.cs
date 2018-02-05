using System;

namespace VehicleDatabase.WebAPI.Models
{
    public class VehicleModelUpdateModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public Guid MakeId { get; set; }
    }
}