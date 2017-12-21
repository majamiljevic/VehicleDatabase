using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleDatabase.Common.Infrastructure
{
    public class Paging : IPaging
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;        
    }
}