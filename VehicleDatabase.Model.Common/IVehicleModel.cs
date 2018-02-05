using System;

namespace VehicleDatabase.Model.Common
{
    public interface IVehicleModel
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }

        IVehicleMake Make { get; set; }
        Guid MakeId { get; set; }
    }
}