using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Vidly.Dtos;
using Vidly.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using System.Linq;
using System;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MoviesController(IMapper mapper)
        {
            _context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());

            _mapper = mapper;
        }

        // GET /api/movies
        [HttpGet]
        public IActionResult GetMovies(string? query = null)
        {
            var moviesQuery = _context.Movies
                .Include(c => c.Genre);

            if (!String.IsNullOrWhiteSpace(query)) 
                moviesQuery = moviesQuery
                    .Where(c => c.Name.Contains(query))
                    .Where(c => c.NumberAvailable > 0)
                    .Include(c => c.Genre);

            var moviesDto = moviesQuery
                .ToList()
                .Select(_mapper.Map<Movie, MovieDto>);

            return Ok(moviesDto);
        }

        //GET /api/movies/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return NotFound();
            return Ok(_mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/movies
        [HttpPost]
        public IActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = _mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.GetDisplayUrl() + "/" + movie.Id), movieDto);
        }

        // PUT /api/movies/{id}
        [HttpPut("{id:int}")]
        public IActionResult UpdateMovie(int id, MovieDto MovieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

           
            _mapper.Map(MovieDto, movieInDb);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/movies/{id}
        [HttpDelete("{id:int}")]
        public IActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

            _context.Movies.Remove(movieInDb);

            _context.SaveChanges();

            return Ok();
        }
    }
}
