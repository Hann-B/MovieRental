using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MovieRentalShop.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? YearReleased { get; set; }
        public string Director { get; set; }
        public int? GenreId { get; set; }
        public Genre Genre { get; set; }
        public bool? IsCheckedIn { get; set; }
        public Movie() { }
        public Movie(SqlDataReader reader, bool hasGenre=true)
        {
            this.Id = (int)reader["Id"];
            this.Name = reader["MName"]?.ToString();
            this.YearReleased = (int?)reader["YearReleased"];
            this.Director = reader["Director"]?.ToString();
            this.GenreId = (int?)reader["GenreId"];
            this.IsCheckedIn = (bool?)reader["IsCheckedIn"];
            if (hasGenre)
            {
               this.Genre = new Genre
                {
                    Id = (int)reader["GenreId"],
                    Name = reader["GName"].ToString()
                };
            }
            else
            {
                this.Genre = new Genre() { Id = (int)reader["GenreId"] };
            }
        }

    }
}