using Application.DTO.Bookings;
using Application.UseCases;
using Application.UseCases.Queries.Apartment;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Implementation.UseCases.Queries.Bookings
{
    public class EfCheckBookingAvailabilityQuery : EfUseCase, ICheckBookingAvailability
    {
        private readonly CheckBookingAvailabilityValidator _validator;
        public EfCheckBookingAvailabilityQuery(BookingContext context, CheckBookingAvailabilityValidator validator) 
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 33;

        public string Name => nameof(EfCheckBookingAvailabilityQuery);

        public IsBookingAvailable Execute(CheckBookingAvailabilityDto data)
        {
            _validator.ValidateAndThrow(data);
            DateTime checkInDate = DateTime.Parse(data.CheckIn);
            DateTime checkOutDate = DateTime.Parse(data.CheckOut);

            bool apartmentFinded = Context.Bookings.Any(
                 b => b.IsActive && b.ApartmentId == data.ApartmentId &&
                ((checkInDate >= b.CheckIn && checkInDate < b.CheckOut) ||
                (checkOutDate > b.CheckIn && checkOutDate <= b.CheckOut) ||
                (checkInDate < b.CheckIn && checkOutDate > b.CheckOut)));


            var tmp = Context.Bookings.Where(
                 b => b.IsActive && b.ApartmentId == data.ApartmentId &&
                ((checkInDate >= b.CheckIn && checkInDate < b.CheckOut) ||
                (checkOutDate > b.CheckIn && checkOutDate <= b.CheckOut) ||
                (checkInDate < b.CheckIn && checkOutDate > b.CheckOut))).ToList();

            var IsAvailable = new IsBookingAvailable
            {
                IsAvailable = !apartmentFinded
            };

            return IsAvailable;
        }
    }
}
