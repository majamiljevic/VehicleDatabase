using System;

namespace VehicleDatabase.Model.Common
{
    public interface IVehicleModel
    {
        string Abrv { get; set; }
        Guid Id { get; set; }
        IVehicleMake Make { get; set; }
        Guid MakeId { get; set; }
        string Name { get; set; }
    }
}