using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleDatabase.Common.Infrastructure
{
    public class Filtering : IFiltering
    {
        public string SearchString { get; set; }
        public Guid? MakeId { get; set; }
    }
}