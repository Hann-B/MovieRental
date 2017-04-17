using MovieRentalShop.Models;
using MovieRentalShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalShop.Controllers
{
    public class MovieController : Controller
    {

        const string connectionString = @"Server=localhost\SQLEXPRESS;Database=MovieRental;Trusted_Connection=True;";
        MovieServices movieService = new MovieServices(connectionString);
        GenreService genreService = new GenreService(connectionString);

        //GET:all movies
        public ActionResult Index()
        {
            var movies = movieService.GetAllMovies();
            return View(movies);
        }
        public ActionResult AdminIndex()
        {
            var movies = movieService.GetAllMovies();
            return View(movies);
        }
        public ActionResult Genre()
        {
            //ViewBag of Genres
            var genres = genreService.GetAllGenres();
            ViewBag.Genres = genres.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name });
            return View();
        }

        public ActionResult Rent(int id)
        {
            var movie = movieService.GetMovie(id);
            return View(movie);
        }
        [HttpPost]
        public ActionResult Rent(int id, FormCollection collection)
        {
            var movie = new Movie();
            movie.Id = id;
            var newMovie = movieService.Rent(movie);
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var Movie = movieService.GetMovie(id);
            return View(Movie);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var movie = new Movie()
            {
                Id = id,
                Name = collection["Name"],
                YearReleased = int.Parse(collection["YearReleased"]),
                Director = collection["Director"].ToString(),
                GenreId = int.Parse(collection["GenreId"]),
                IsCheckedIn=bool.Parse(collection["IsCheckedIn"])
            };
            var newMovie = movieService.Edit(id);
            return RedirectToAction("Index");
        }
    }
}