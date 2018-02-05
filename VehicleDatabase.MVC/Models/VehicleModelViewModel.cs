using System;
using System.ComponentModel.DataAnnotations;

namespace VehicleDatabase.MVC.Models
{
    public class VehicleModelViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Manufacturer is required 2")]
        public Guid SelectedMakeId { get; set; }

        [Required(ErrorMessage = "Manufacturer is required")]
        public string SelectedMakeName { get; set; }

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

        public string Makes { get; set; }
    }
}