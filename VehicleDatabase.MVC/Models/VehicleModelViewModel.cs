using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace VehicleDatabase.MVC.Models
{
    public class VehicleModelViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Manufacturer is required")]
        public Guid MakeId { get; set; }

        public VehicleMakeViewModel Make { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(30)]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Invalid input")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Abbreviation is required")]
        [StringLength(30)]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Invalid input")]
        [Display(Name = "Abbreviation")]
        public string Abrv { get; set; }

        public IEnumerable<VehicleMakeViewModel> AllMakes { get; set; }
    }
}