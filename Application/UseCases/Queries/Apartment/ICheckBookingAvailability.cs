using Application.DTO.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Apartment
{
    public interface ICheckBookingAvailability : IQuery<IsBookingAvailable, CheckBookingAvailabilityDto>
    {
    }
}
