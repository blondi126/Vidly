using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Vidly.Data;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRentalsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public NewRentalsController(IMapper mapper)
        {
            _context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
            _mapper = mapper;
        }

        // GET /api/newrentals
        [HttpGet("{id:int}")]
        public IActionResult GetRentals(int id)
        {
            var rentals = _context.Rentals.Where(c => c.Id == id);

            List<string> movies = new List<string>();

            foreach (var rental in rentals)
            {
                string name = _context.Movies.Single(c => c.Id == rental.Movie.Id).Name;
                movies.Add(name);
            }

            return Ok(movies);
        }

        // POST /api/newrentals
        [HttpPost]
        public IActionResult CreateNewRentals(NewRentalDto newRental)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Customer customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);

            List<Movie> movies = _context.Movies.Where(c => newRental.MovieIds.Contains(c.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now,
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
