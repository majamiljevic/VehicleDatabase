using System;

namespace VehicleDatabase.Common.Infrastructure
{
    public interface IFiltering
    {
        Guid? MakeId { get; set; }
        string SearchString { get; set; }
    }
}