using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace VehicleDatabase.Service.DAL
{
    public class VehicleDatabaseDBContext : DbContext
    {
        public DbSet<VehicleMake> Make { get; set; }
        public DbSet<VehicleModel> Model { get; set; }

        public VehicleDatabaseDBContext() : base("name=VehicleDBConnectionString")
        {
        }
    }
}