using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;



namespace VehicleDatabase.DAL
{
    public class VehicleDatabaseDBContext : DbContext
    {
        public DbSet<VehicleMakeEntity> Make { get; set; }
        public DbSet<VehicleModelEntity> Model { get; set; }

        public VehicleDatabaseDBContext() : base("name=VehicleDBConnectionString")
        {
        }
    }
}