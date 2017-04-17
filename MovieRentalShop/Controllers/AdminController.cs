using MovieRentalShop.Models;
using MovieRentalShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalShop.Controllers
{
    public class AdminController : Controller
    {
        const string connectionString = @"Server=localhost\SQLEXPRESS;Database=MovieRental;Trusted_Connection=True;";
        MovieServices movieService = new MovieServices(connectionString);

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Rented()
        {
            //return list of rented movies
            var movies = movieService.RentedMovies();
            return View(movies);
        }

        public ActionResult Return(int id)
        {
            var movie = movieService.GetMovie(id);
            return View(movie);
        }
        [HttpPost]
        public ActionResult Return(int id, FormCollection collection)
        {
            var movie = new Movie();
            movie.Id = id;
            var newMovie = movieService.Return(movie);
            return RedirectToAction("Rented");
        }
    }
}