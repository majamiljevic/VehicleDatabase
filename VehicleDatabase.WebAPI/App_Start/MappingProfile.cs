using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleDatabase.DAL;
using VehicleDatabase.Model.Common;
using VehicleDatabase.WebAPI.Models;

namespace VehicleDatabase.WebAPI.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleMakeModel, IVehicleMake>().ReverseMap();
            CreateMap<VehicleModelModel, IVehicleModel>().ReverseMap();
 
            CreateMap<VehicleModelUpdateModel, IVehicleModel>().ReverseMap();
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
