using System;

namespace VehicleDatabase.Common.Infrastructure
{
    public class Filtering : IFiltering
    {
        public string SearchString { get; set; }
        public Guid? MakeId { get; set; }
    }
}