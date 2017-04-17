using MovieRentalShop.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalShop.Services
{
    public class RentalLogService
    {
        private string _ConnectionString;
        public RentalLogService(string connectionString)
        {
            this._ConnectionString = connectionString;
        }

        public List<RentalLog> GetRentalLog()
        {
            var rv = new List<RentalLog>();
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"SELECT * FROM RentalLog
                    JOIN Movies ON Movies.Id=MovieId
                    JOIN Customer ON Customer.Id = CustomerId
                    ORDER BY DueBackDate";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rv.Add(new RentalLog(reader));
                }
                connection.Close();
            }
            return rv;
        }

        public RentalLog Create(RentalLog RentalLog)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"INSERT INTO RentalLog
                            ([MovieId]
                            ,[CustomerId]
                            ,[CheckOutDate]
                            ,[DueBackDate])
                            VALUES(@MovieId, @CustomerId, @CheckOutDate, @DueBackDate)";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@MovieId", RentalLog.MovieId);
                cmd.Parameters.AddWithValue("@CustomerId", RentalLog.CustomerId);
                cmd.Parameters.AddWithValue("@CheckOutDate", RentalLog.CheckOutDate);
                cmd.Parameters.AddWithValue("@DueBackDate", RentalLog.DueBackDate);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return RentalLog;
        }

        public RentalLog GetEntry(int id)
        {
            var rentalLog = new List<RentalLog>();
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"SELECT * FROM RentalLog 
                            JOIN Movies ON Movies.Id=MovieId
                            JOIN Genre ON Genre.Id=Movies.GenreId
                            JOIN Customer ON Customer.Id = CustomerId
                            WHERE RentalLog.Id=@Id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rentalLog.Add(new RentalLog(reader));
                }
                connection.Close();
            }
            return rentalLog[0];
        }

    }
}