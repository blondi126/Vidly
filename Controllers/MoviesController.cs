 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [Authorize]
    public class MoviesController : Controller 
    {
        private readonly ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public IActionResult Index()
        {   
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public IActionResult New()
        {
            var genres = _context.Genre.ToList();
            var viewModel = new MovieFormViewModel
            {
                Movie = new Movie()
                {
                    ReleaseDate = null,
                },
                Genres = genres
            };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = _context.Genre.ToList()
                };
                return View("MovieForm", viewModel);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);

                //Mapper.Map(customer, customerInDb);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.Genre = movie.Genre;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.DateAdded = movie.DateAdded;

            }
            _context.SaveChanges();
            return RedirectToAction("index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id );

            if (movie == null)
                return NotFound();

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genre.ToList()
            };
            return View("MovieForm", viewModel);
        }

        //[Authorize(Roles = RoleName.CanManageMovies)]
        //[Route("Movies/Details/{id}")]
        //public IActionResult Details(int id)
        //{

        //    foreach (var movie in _context.Movies.Include(c => c.Genre))
        //    {
        //        if (movie.Id == id)
        //        {
        //            return View(movie);
        //        }
        //    }
        //    return NotFound();
        //}
    }
}
