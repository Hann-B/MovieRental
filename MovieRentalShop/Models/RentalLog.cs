using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MovieRentalShop.Models
{
    public class RentalLog
    {
        public int Id { get; set; }
        public int? MovieId { get; set; }
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string NeatCheckOutDate
        {
            get
            {
                if (CheckOutDate.HasValue)
                {
                    return ((DateTime)this.CheckOutDate).ToShortDateString();

                }
                else
                {
                    return null;
                }
            }
        }
        public DateTime? DueBackDate { get; set; }
        public string NeatDueBackDate
        {
            get
            {
                if (DueBackDate.HasValue)
                {
                    return ((DateTime)this.DueBackDate).ToShortDateString();
                }
                else
                {
                    return null;
                }
            }
        }

        public RentalLog() { }
        public RentalLog(SqlDataReader reader, bool hasMovie=true, bool hasCustomer=true, bool hasGenre=true)
        {
            this.Id = (int)reader["Id"];
            this.MovieId = (int?)reader["MovieId"];
            this.CustomerId = (int?)reader["CustomerId"];
            this.CheckOutDate = reader["CheckOutDate"] as DateTime?;
            this.DueBackDate = reader["DueBackDate"] as DateTime?;
            if (hasMovie)
            {
                this.Movie = new Movie
                {
                    Id = (int)reader["MovieId"],
                    Name = reader["MName"].ToString(),
                    YearReleased = (int?)reader["YearReleased"],
                    Director = reader["Director"]?.ToString(),
                    GenreId = (int?)reader["GenreId"],
                    IsCheckedIn = (bool?)reader["IsCheckedIn"]
            };
            }
            else
            {
                this.Movie = new Movie() { Id = (int)reader["MovieId"] };
            }
            if (hasCustomer)
            {
                this.Customer = new Customer
                {
                    Id = (int)reader["CustomerId"],
                    Name = reader["Name"].ToString(),
                    Email = reader["Email"]?.ToString(),
                    PhoneNumber = reader["PhoneNumber"]?.ToString()
                };
            }
            else
            {
                this.Customer = new Customer() { Id = (int)reader["CustomerId"] };
            }
        }
    }
}