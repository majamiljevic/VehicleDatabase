using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(VehicleDatabase.SPA.Startup))]

namespace VehicleDatabase.SPA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
