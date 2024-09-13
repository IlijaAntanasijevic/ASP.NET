using Application.DTO;
using Application.DTO.Apartments;
using Application.DTO.Search;
using Application.UseCases.Queries.Apartment;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Apartments
{
    public class EfGetApartmentsQuery : EfUseCase, IGetApartmentsQuery
    {
        public EfGetApartmentsQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 15;

        public string Name => nameof(EfGetApartmentsQuery);

        public PagedResponse<SearchApartmentsDto> Execute(ApartmentSearch search)
        {
            var query = Context.Apartments.Where(x => x.IsActive).AsQueryable();
            if(search.UserId.HasValue)
            {
                query = query.Where(x => x.UserId ==  search.UserId);
            }

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));
            }

            if (search.ApartmentTypeId.HasValue)
            {
                query = query.Where(x => x.ApartmentTypeId == search.ApartmentTypeId.Value);
            }

            if(search.CountryId.HasValue)
            {
                query = query.Where(x => x.CityCountry.CountryId == search.CountryId.Value);
            }
            if (search.CityId.HasValue)
            {
                query = query.Where(x => x.CityCountry.CityId == search.CityId.Value);
            }
            if (search.Guests.HasValue)
            {
                query = query.Where(x => x.MaxGuests >= search.Guests.Value);
            }

            if (!string.IsNullOrEmpty(search.CheckIn) && !string.IsNullOrEmpty(search.CheckOut))
            {
                DateTime checkInDate = DateTime.Parse(search.CheckIn);
                DateTime checkOutDate = DateTime.Parse(search.CheckOut);

                query = query.Where(x => !x.Bookings.Any(b => b.IsActive &&
                    ((checkInDate >= b.CheckIn && checkInDate < b.CheckOut) ||
                    (checkOutDate > b.CheckIn && checkOutDate <= b.CheckOut) ||
                    (checkInDate < b.CheckIn && checkOutDate > b.CheckOut))));
            }

            if (search.Sorts != null && !search.Sorts.Any())
            {
                query = query.OrderByDescending(x => x.CreatedAt);
            }
            else
            {
                if(search.Sorts.Any(x => x.SortProperty == "price"))
                {
                    if(search.Sorts.FirstOrDefault(x => x.SortProperty == "price").Direction == SortDirection.Asc)
                    {
                        query = query.OrderBy(x => x.Price);
                    }     
                    else
                    {
                        query = query.OrderByDescending(x => x.Price);
                    }
                }
                if(search.Sorts.Any(x => x.SortProperty == "popular"))
                {
                    query = query.OrderByDescending(x => x.Bookings.Count);
                }
            }

            return query.AsPagedReponse(search, x => new SearchApartmentsDto
            {
                Name = x.Name,
                City = x.CityCountry.City.Name,
                Country = x.CityCountry.Country.Name,
                Id = x.Id,
                ApartmentType = x.ApartmentType.Name,
                MainImage = x.MainImage,
                MaxGuests = x.MaxGuests,
                PricePerNight = x.Price,
                TotalBookings = x.Bookings.Count
            });


        }
    }
}
