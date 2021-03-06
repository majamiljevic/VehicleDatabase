﻿using System;
using System.Collections.Generic;

namespace VehicleDatabase.Service
{
    public interface IVehicleMake
    {
        string Abrv { get; set; }
        Guid Id { get; set; }
        IEnumerable<VehicleModel> Model { get; set; }
        string Name { get; set; }
    }
}