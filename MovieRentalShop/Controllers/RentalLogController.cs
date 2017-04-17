using MovieRentalShop.Models;
using MovieRentalShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalShop.Controllers
{
    public class RentalLogController : Controller
    {
        const string connectionString = @"Server=localhost\SQLEXPRESS;Database=MovieRental;Trusted_Connection=True;";
        RentalLogService rentalLogService = new RentalLogService(connectionString);
        
        // GET: RentalLog
        public ActionResult Index()
        {
            var RentalLog = rentalLogService.GetRentalLog();
            return View(RentalLog);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(int id, FormCollection collection)
        {
            var rentalLog = new RentalLog
            {
                MovieId = int.Parse(collection["MovieId"]),
                CustomerId = int.Parse(collection["CustomerId"]),
                CheckOutDate = DateTime.Parse(collection["CheckOutDate"]),
                DueBackDate=DateTime.Parse(collection["DueBackDate"])
            };
            rentalLogService.Create(rentalLog);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var Entry = rentalLogService.GetEntry(id);
            return View(Entry);
        }
    }
}