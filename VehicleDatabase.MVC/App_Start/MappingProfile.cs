using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using VehicleDatabase.MVC.Models;
using VehicleDatabase.Service;

namespace VehicleDatabase.MVC.App_Start
{
    public class MappingProfile : Profile
    { 
        public MappingProfile()
        {
            CreateMap<VehicleMakeViewModel, VehicleMake>().ReverseMap();
            CreateMap<VehicleModelViewModel, VehicleModel>().ReverseMap();
            CreateMap<VehicleMake, VehicleMakeEntity>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelEntity>().ReverseMap();
        }
    }
}