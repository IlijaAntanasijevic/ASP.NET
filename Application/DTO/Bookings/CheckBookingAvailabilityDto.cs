using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Bookings
{
    public class CheckBookingAvailabilityDto
    {
        public string CheckIn {  get; set; }
        public string CheckOut { get; set; }
        public int ApartmentId { get; set; }
    }
}
