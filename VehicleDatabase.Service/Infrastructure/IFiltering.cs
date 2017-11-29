using System;

namespace VehicleDatabase.Service.Infrastructure
{
    public interface IFiltering
    {
        Guid? MakeId { get; set; }
        string SearchString { get; set; }
    }
}