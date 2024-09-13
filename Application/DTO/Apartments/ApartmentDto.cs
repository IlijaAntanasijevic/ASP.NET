using Application.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Apartments
{
    public class SearchApartmentsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxGuests { get; set; }
        public decimal PricePerNight { get; set; }
        public string MainImage { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ApartmentType { get; set; }
        public int TotalBookings { get; set; }
    }

    public class ApartmentDto : SearchApartmentsDto
    {
        public string Description { get; set; }
         public UserDto User { get; set; }
        public IEnumerable<string> PaymentMethods { get; set; }
        public IEnumerable<string> Features { get; set; }
        public IEnumerable<string> Images { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public IEnumerable<int> PaymentMethodIds { get; set; }
        public IEnumerable<int> FeaturesIds { get; set; }
        public string Address { get; set; }
        public int ApartmentTypeId { get; set; }

    }

   
}
