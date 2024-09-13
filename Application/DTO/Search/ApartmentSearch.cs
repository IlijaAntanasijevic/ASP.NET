using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Search
{
    public class ApartmentSearch : PagedSearch
    {
        public int? UserId { get; set; }
        public string Keyword { get; set; }
        public List<SortBy> Sorts { get; set; } = new List<SortBy>();
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? ApartmentTypeId { get; set; }
        public int? Guests { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
    }
}
