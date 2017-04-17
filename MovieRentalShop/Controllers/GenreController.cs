using MovieRentalShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRentalShop.Services;

namespace MovieRentalShop.Controllers
{
    public class GenreController : Controller
    {
        const string connectionString = @"Server=localhost\SQLEXPRESS;Database=MovieRental;Trusted_Connection=True;";
        GenreService genreService = new GenreService(connectionString);

        public ActionResult Index()
        {
            var genre = genreService.GetAllGenres();
            return View(genre);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var genre = new Genre
            {
                Name = collection["Name"],
            };
            genreService.Create(genre);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var Genre = genreService.GetGenre(id);
            return View(Genre);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var Genre = new Genre();
            Genre.Id = id;
            var genre = genreService.Delete(Genre);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var Genre = genreService.GetGenre(id);
            return View(Genre);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var genre = new Genre()
            {
                Id = id,
                Name = collection["Name"]
            };
            var newGenre = genreService.Edit(genre);
            return RedirectToAction("Index");
        }
    }
}