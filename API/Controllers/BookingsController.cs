﻿using Application.DTO.Bookings;
using Application.DTO.Search;
using Application.UseCases.Commands.Bookings;
using Application.UseCases.Queries.Apartment;
using Application.UseCases.Queries.Bookings;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public BookingsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<BookingsController>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] BookingSearch search, [FromServices] IGetBookingsQuery query)
        {
            return Ok(_handler.HandleQuery(query,search));
        }
          

        // GET api/<BookingsController>/5
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id, [FromServices] IFindBookingQuery query)
        {
            
            return Ok(_handler.HandleQuery(query, id));
        }

        // POST api/<BookingsController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] BookingDto data, ICreateBookingCommand command)
        {
            _handler.HandleCommand(command,data);
            return Created();
        }

        // PUT api/<BookingsController>/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] EditBookingDto data, [FromServices] IUpdateBookingCommand command)
        {
            data.BookingId = id;
            _handler.HandleCommand(command,data);
            return NoContent();
        }

        // DELETE api/<BookingsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id, [FromServices] IDeleteBookingCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }

        [HttpPost("/api/booking/availability")]
  
        public IActionResult CheckBooking([FromBody] CheckBookingAvailabilityDto data, [FromServices] ICheckBookingAvailability query) 
        {
            return Ok(_handler.HandleQuery(query, data));
        }
    }
}
