using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleDatabase.MVC.Models
{
    public class VehicleMakeViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(30)]
        [RegularExpression (@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Invalid input")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Abbreviation is required")]
        [StringLength(30)]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Invalid input")]
        [Display(Name = "Abbreviation")]
        public string Abrv { get; set; }

        public virtual IEnumerable<VehicleModelViewModel> Model { get; set; }
    }
}