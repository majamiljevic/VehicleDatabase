using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using VehicleDatabase.MVC.Models;
using VehicleDatabase.Service;
using VehicleDatabase.Model;
using VehicleDatabase.Model.Common;
using VehicleDatabase.DAL;

namespace VehicleDatabase.MVC.App_Start
{
    public class MappingProfile : Profile
    { 
        public MappingProfile()
        {
            CreateMap<VehicleMakeViewModel, IVehicleMake>().ReverseMap();
            CreateMap<VehicleModelViewModel, IVehicleModel>().ReverseMap();
            CreateMap<IVehicleMake, VehicleMakeEntity>().ReverseMap();
            CreateMap<IVehicleModel, VehicleModelEntity>().ReverseMap();
        }
    }
}