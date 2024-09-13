using Application.DTO.Apartments;
using Application.DTO.Bookings;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Implementation.Validators
{
    public class CheckBookingAvailabilityValidator : AbstractValidator<CheckBookingAvailabilityDto>
    {
        public CheckBookingAvailabilityValidator(BookingContext context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.CheckIn)
                 .NotEmpty()
                 .WithMessage("Check-in date is required.")
                 .Must(BeAFutureDate)
                 .WithMessage("Check-in date must be in the future.");

            RuleFor(x => x.CheckOut)
                .NotEmpty()
                .WithMessage("Check-out date is required.")
                .Must(BeAFutureDate)
                .WithMessage("Check-out date must be in the future.")
                .GreaterThan(x => x.CheckIn)
                .WithMessage("Check-out date must be later than check-in date.");

            RuleFor(x => x.ApartmentId).NotEmpty()
                                      .WithMessage("Apartment id is required")
                                      .Must(id => context.Apartments.Any(x => x.Id == id && x.IsActive))
                                      .WithMessage("Apartment doesn't exist");


        }


        private bool BeAFutureDate(string dateStr)
        {
            DateTime date = DateTime.Parse(dateStr);
            return date > DateTime.Now;
        }

    }
}
