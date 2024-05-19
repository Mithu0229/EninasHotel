using EninasHotel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EninasHotel.Application.Common.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        void Update(Booking entity);
       
    }
}
