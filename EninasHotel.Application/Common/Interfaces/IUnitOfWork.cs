﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EninasHotel.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IVillaRepository Villa { get; }
        IAmountRepository Amount { get; }
        IVillaNumberRepository VillaNumber { get; }
        IBookingRepository Booking { get; }
        IAmenityRepository Amenity { get; }

        IApplicationUserRepository User { get; }
        void Save();
    }
}
