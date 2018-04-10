using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleDatabase.DAL
{
    public class VehicleModelEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        [ForeignKey("Make")]
        public Guid MakeId { get; set; }
        public  virtual VehicleMakeEntity Make { get; set; }
    }
}