using Application.DTO;
using Application.UseCases.Queries.Lookup;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Lookup
{
    public class EfGetCountryCitiesQuery : EfUseCase, IGetCountryCitiesQuery
    {

        public EfGetCountryCitiesQuery(BookingContext context) : base(context)
        {

        }

        public int Id => 32;

        public string Name => nameof(EfGetCountryCitiesQuery);

        public IEnumerable<BasicDto> Execute(int id)
        {
            var cityCountries = Context.CitiesCountry.Include(x => x.City);
            var tmp = cityCountries.ToList();
            var cities = cityCountries.Where(x => x.CountryId == id).Select(x => new BasicDto
            {
                Id = x.City.Id,
                Name = x.City.Name
            }).ToList();

            return cities;
        }
    }
}
