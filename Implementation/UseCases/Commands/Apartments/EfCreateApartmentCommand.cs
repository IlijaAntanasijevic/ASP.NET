﻿using App.Domain;
using Application;
using Application.DTO;
using Application.DTO.Apartments;
using Application.UseCases.Commands.Apartments;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Apartments
{
    public class EfCreateApartmentCommand : EfUseCase, ICreateApartmentCommand
    {
        private readonly CreateApartmentValidator _validator;
        private readonly IApplicationActor _actor;
        private readonly IFileUploader _fileUploader;
        public EfCreateApartmentCommand(BookingContext context, CreateApartmentValidator validator, IApplicationActor actor, IFileUploader uploader)
            : base(context)
        {
            _validator = validator;
            _actor = actor;
            _fileUploader = uploader;
        }

        public int Id => 9;

        public string Name => nameof(EfCreateApartmentCommand);

        public void Execute(CreateApartmentDto data)
        {
            _validator.ValidateAndThrow(data);

            var mainImagePath = _fileUploader.Upload(data.MainImage, UploadType.MainImage);
            var otherImages = _fileUploader.Upload(data.Images, UploadType.Apartment);

            var cityCountry = Context.CitiesCountry.FirstOrDefault(x => x.CityId == data.CityId && x.CountryId == x.CountryId);
            
            var cityCountryId = cityCountry.Id;
            var cityId = cityCountry.CityId;
            var countryId = cityCountry.CountryId;

            var apartmentToAdd = new Apartment
            {
                UserId = _actor.Id,
                Name = data.Name,
                Description = data.Description,
                Address = data.Address,
                CityCountryId = cityCountryId,
                MaxGuests = data.MaxGuests,
                Price = data.Price,
                ApartmentTypeId = data.ApartmentTypeId,
                MainImage = mainImagePath,
                FeatureApartments = data.FeatureIds.Select(x => new FeatureApartment
                {
                    FeatureId = x
                }).ToList(),
                PaymentApartments = data.PaymentMethodIds.Select(x => new PaymentApartment
                {
                     PaymentId = x
                }).ToList(),
                Images = otherImages.Select(x => new Image
                {
                     Path = x
                }).ToList()
       
            };

            Context.Apartments.Add(apartmentToAdd);
            Context.SaveChanges();


            
        }
    }
}
