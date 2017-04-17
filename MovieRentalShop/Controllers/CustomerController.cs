using MovieRentalShop.Models;
using MovieRentalShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalShop.Controllers
{
    public class CustomerController : Controller
    {
        const string connectionString = @"Server=localhost\SQLEXPRESS;Database=MovieRental;Trusted_Connection=True;";
        CustomerServices customerService = new CustomerServices(connectionString);

        // GET: Customer
        public ActionResult Index()
        {
            //List all custmers
            var customer = customerService.GetAllCustomers();
            return View(customer);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(FormCollection collection)
        {
            var customer = new Customer
            {
                Name = collection["Name"],
                Email = collection["Email"],
                PhoneNumber=collection["PhoneNumber"]
            };
                customerService.Add(customer);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var Customer = customerService.GetCustomer(id);
            return View(Customer);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var Customer = new Customer();
            Customer.Id = id;
            var customer = customerService.Delete(Customer);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var Customer = customerService.GetCustomer(id);
            return View(Customer);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var customer = new Customer()
            {
                Id = id,
                Name = collection["Name"],
                Email = collection["Email"],
                PhoneNumber = collection["PhoneNumber"]
            };
            var newCustomer = customerService.Edit(customer);
            return RedirectToAction("Index");
        }
    }
}