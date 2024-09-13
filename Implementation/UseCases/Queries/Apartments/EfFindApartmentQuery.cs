﻿using App.Domain;
using Application.DTO.Apartments;
using Application.DTO.Users;
using Application.Exceptions;
using Application.UseCases.Queries.Apartment;
using DataAccess;
using Microsoft.EntityFrameworkCore;


namespace Implementation.UseCases.Queries.Apartments
{
    public class EfFindApartmentQuery : EfUseCase, IFindApartmentQuery
    {
        public EfFindApartmentQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 16;

        public string Name => nameof(EfFindApartmentQuery);

        public ApartmentDto Execute(int search)
        {
            var apartment = Context.Apartments.Include(x => x.CityCountry)
                                                  .ThenInclude(cc => cc.City)
                                                  .Include(x => x.CityCountry)
                                                  .ThenInclude(cc => cc.Country)
                                                  .Include(x => x.User)
                                                  .Include(x => x.ApartmentType)
                                                  .Include(x => x.FeatureApartments)
                                                  .ThenInclude(f => f.Feature)
                                                  .Include(x => x.Images)
                                                  .Include(x => x.PaymentApartments)
                                                  .ThenInclude(p => p.Payment)
                                                  .FirstOrDefault(x => x.Id == search); 


            if (apartment == null)
            {
                throw new EntityNotFoundException(nameof(Apartment),search);
            }

            var apartmentDto = new ApartmentDto
            {
                Id = apartment.Id,
                City = apartment.CityCountry.City.Name,
                Name = apartment.Name,
                Description = apartment.Description,
                Country = apartment.CityCountry.Country.Name,
                MainImage = apartment.MainImage,
                MaxGuests = apartment.MaxGuests,
                PricePerNight = apartment.Price,
                User = new UserDto
                {
                    Email = apartment.User.Email,
                    FirstName = apartment.User.FirstName,
                    LastName = apartment.User.LastName,
                    Phone = apartment.User.Phone,
                    Avatar = apartment.User.Avatar,
                    Id = apartment.Id,
                },
                ApartmentType = apartment.ApartmentType.Name,
                Features = apartment.FeatureApartments.Select(x => x.Feature.Name).ToList(),
                Images = apartment.Images.Select(x => x.Path).ToList(),
                PaymentMethods = apartment.PaymentApartments.Select(x => x.Payment.Name).ToList(),
                CityId = apartment.CityCountry.CityId,
                CountryId = apartment.CityCountry.CountryId,
                FeaturesIds = apartment.FeatureApartments.Select(x => x.FeatureId).ToList(),
                PaymentMethodIds = apartment.PaymentApartments.Select(x => x.PaymentId).ToList(),
                Address = apartment.Address,
                ApartmentTypeId = apartment.ApartmentTypeId
            };

            return apartmentDto;
        }
    }
}
