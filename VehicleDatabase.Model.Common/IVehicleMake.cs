using System;
using System.Collections.Generic;

namespace VehicleDatabase.Model.Common
{
    public interface IVehicleMake
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }

        IEnumerable<IVehicleModel> Model { get; set; }
    }
}