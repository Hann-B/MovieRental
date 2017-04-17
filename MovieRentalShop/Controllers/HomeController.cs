using MovieRentalShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MovieRentalShop.Controllers
{
    public class HomeController : Controller
    {
        const string connectionString = @"Server=localhost\SQLEXPRESS;Database=MovieRental;Trusted_Connection=True;";
        GenreService genreService = new GenreService(connectionString);

        public ActionResult Index()
        {
            return View();
        }
        //DropDownList:Select Genre
        public ActionResult Select()
        {
            //populate the viewbag with a list of genres
            var genre = genreService.GetAllGenres();
            ViewBag.Genre = genre.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name });
            return View();
        }
        
    }
}