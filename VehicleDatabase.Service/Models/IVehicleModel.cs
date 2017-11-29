using System;

namespace VehicleDatabase.Service
{
    public interface IVehicleModel
    {
        string Abrv { get; set; }
        Guid Id { get; set; }
        VehicleMake Make { get; set; }
        Guid MakeId { get; set; }
        string Name { get; set; }
    }
}