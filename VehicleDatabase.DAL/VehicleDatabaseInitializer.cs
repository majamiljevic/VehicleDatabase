using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace VehicleDatabase.DAL
{
    public class VehicleDatabaseInitializer : DropCreateDatabaseAlways<VehicleDatabaseDBContext>
    {
        protected override void Seed(VehicleDatabaseDBContext context)
        {
            var manufacturers = new List<VehicleMakeEntity>
            {
                new VehicleMakeEntity {Id = Guid.Parse("40d6c5dd-4343-46bf-ba75-27d977ba4451"), Name ="SEAT SA", Abrv ="Seat"},
                new VehicleMakeEntity {Id = Guid.Parse("77bd6245-8356-4eb4-8805-792b073f4766"), Name ="Chevrolet Motor Car Company", Abrv ="Chevrolet"},
                new VehicleMakeEntity {Id = Guid.Parse("5de857d0-3d12-4873-93a5-98030c6b530c"), Name ="Ford Motor Company", Abrv ="Ford"},
                new VehicleMakeEntity {Id = Guid.Parse("80738e64-ca39-4b0b-8df8-a28921a1271e"), Name ="Hyundai Motor Company", Abrv ="Hyundai"},
                new VehicleMakeEntity {Id = Guid.Parse("37a64439-4717-4664-a388-0c94c44890a4"), Name ="Opel Automobile GmbH", Abrv ="Opel"},
                new VehicleMakeEntity {Id = Guid.Parse("04fe97dc-2a0c-4ca6-8122-1c2d538341d7"), Name ="Mercedes Benz", Abrv ="Mercedes"},
                new VehicleMakeEntity {Id = Guid.Parse("cc101492-dc4b-4200-bfa5-4ef0c543913d"), Name ="Honda Motor Co", Abrv ="Honda"},
                new VehicleMakeEntity {Id = Guid.Parse("60be5245-bcba-431b-b8fa-da6a613a138f"), Name ="Fiat Automobiles SpA.", Abrv ="Fiat"},
                new VehicleMakeEntity {Id = Guid.Parse("833a46ff-291a-4fc3-90bd-cb466be8fc95"), Name ="Volkswagen", Abrv ="VW"},
                new VehicleMakeEntity {Id = Guid.Parse("3de0490f-b673-4a46-9f64-4e8a83fe2886"), Name ="Mazda Motor Corporation", Abrv ="Mazda"},
                new VehicleMakeEntity {Id = Guid.Parse("964178c4-77af-434c-8e93-38cbcb12a94b"), Name ="Alfa Romeo Automobiles SpA", Abrv ="Alfa Romeo"},
                new VehicleMakeEntity {Id = Guid.Parse("360cf56d-ec90-4f7f-863f-cb8e6407e469"), Name ="Bavarian Motor Works AG", Abrv ="BMW"},
                new VehicleMakeEntity {Id = Guid.Parse("c45c0791-f7a7-49b4-8f45-3baf986d66e8"), Name ="Toyota Motor Corporation", Abrv ="Toyota"},
            };

            manufacturers.ForEach(m => context.Make.Add(m));
            context.SaveChanges();


            var vehicleModels = new List<VehicleModelEntity>
            {
                new VehicleModelEntity {MakeId = Guid.Parse("40d6c5dd-4343-46bf-ba75-27d977ba4451"), Name ="SEAT Ateca", Abrv ="Ateca" },
                new VehicleModelEntity {MakeId = Guid.Parse("40d6c5dd-4343-46bf-ba75-27d977ba4451"), Name ="SEAT Leon X PERIENCE", Abrv ="Leon X PERIENCE" },
                new VehicleModelEntity {MakeId = Guid.Parse("5de857d0-3d12-4873-93a5-98030c6b530c"), Name ="Ford Focus", Abrv ="Focus" },
                new VehicleModelEntity {MakeId = Guid.Parse("5de857d0-3d12-4873-93a5-98030c6b530c"), Name ="Ford Fusion", Abrv ="Fusion" },
                new VehicleModelEntity {MakeId = Guid.Parse("37a64439-4717-4664-a388-0c94c44890a4"), Name ="Opel Astra", Abrv ="Astra" },
                new VehicleModelEntity {MakeId = Guid.Parse("37a64439-4717-4664-a388-0c94c44890a4"), Name ="Opel Insignia", Abrv ="Insignia" },
                new VehicleModelEntity {MakeId = Guid.Parse("80738e64-ca39-4b0b-8df8-a28921a1271e"), Name ="Hyundai Tucson", Abrv ="Tucson" },
                new VehicleModelEntity {MakeId = Guid.Parse("80738e64-ca39-4b0b-8df8-a28921a1271e"), Name ="Hyundai i20 Active", Abrv ="i20 Active" },
                new VehicleModelEntity {MakeId = Guid.Parse("60be5245-bcba-431b-b8fa-da6a613a138f"), Name ="Fiat panda", Abrv ="Panda" },
                new VehicleModelEntity {MakeId = Guid.Parse("833a46ff-291a-4fc3-90bd-cb466be8fc95"), Name ="VW Golf GTI", Abrv ="Golf GTI" },
                new VehicleModelEntity {MakeId = Guid.Parse("3de0490f-b673-4a46-9f64-4e8a83fe2886"), Name ="Mazda CX 3", Abrv ="CX 3" },
                new VehicleModelEntity {MakeId = Guid.Parse("c45c0791-f7a7-49b4-8f45-3baf986d66e8"), Name ="Toyota Avalon", Abrv ="Avalon" },
                new VehicleModelEntity {MakeId = Guid.Parse("c45c0791-f7a7-49b4-8f45-3baf986d66e8"), Name ="Toyota Yaris", Abrv ="Yaris" },
            };

            vehicleModels.ForEach(m => context.Model.Add(m));
            context.SaveChanges();
        }
    }
}