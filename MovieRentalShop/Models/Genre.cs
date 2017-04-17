using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalShop.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Genre()
        {
            var genre = new List<SelectListItem>();
        }
        public IEnumerable<SelectListItem> genre { get; set; }
        public Genre(SqlDataReader reader)
        {
            this.Id = (int)reader["Id"];
            this.Name = reader["GName"]?.ToString();
        }
    }
}