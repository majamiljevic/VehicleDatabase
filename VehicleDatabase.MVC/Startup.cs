using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VehicleDatabase.MVC.Startup))]
namespace VehicleDatabase.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
