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
using PagedList;

namespace VehicleDatabase.MVC.App_Start
{
    public class MappingProfile : Profile
    { 
        public MappingProfile()
        {
            CreateMap<VehicleMakeViewModel, IVehicleMake>().ReverseMap();
            CreateMap<VehicleModelViewModel, IVehicleMake>().ReverseMap();
            CreateMap<VehicleModelViewModel, IVehicleModel>()
                .ForPath(dest => dest.Make.Name, opts => opts.MapFrom(src => src.SelectedMakeName))
                .ForMember(dest => dest.MakeId, opts => opts.MapFrom(src => src.SelectedMakeId))
                .ReverseMap();
            CreateMap<IVehicleMake, VehicleMakeEntity>().ReverseMap();
            CreateMap<IVehicleModel, VehicleModelEntity>().ReverseMap();
            CreateMap(typeof(IPagedList<>), typeof(IPagedList<>)).ConvertUsing(typeof(IPagedListConverter<,>));
        }
    }

    public class IPagedListConverter<TSource, TDestination> : ITypeConverter<IPagedList<TSource>, IPagedList<TDestination>> where TSource : class where TDestination : class
    {
        public IPagedList<TDestination> Convert(IPagedList<TSource> source, IPagedList<TDestination> destination, ResolutionContext context)
        {
            var collection = Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
            return new StaticPagedList<TDestination>(collection, source.GetMetaData());
        }
    }
}