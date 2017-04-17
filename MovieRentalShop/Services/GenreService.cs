using MovieRentalShop.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRentalShop.Services
{
    public class GenreService
    {
        private string _ConnectionString;
        public GenreService(string connectionString)
        {
            this._ConnectionString = connectionString;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            var rv = new List<Genre>();
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = "SELECT * FROM Genre ORDER BY GName";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rv.Add(new Genre(reader));
                }
                connection.Close();
            }
            return rv;
        }

        public Genre Create(Genre Genre)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"INSERT INTO Genre
                            ([GName])
                            VALUES(@Name)";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Name", Genre.Name);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return Genre;
        }

        public Genre GetGenre(int id)
        {
            var genre = new List<Genre>();
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"SELECT * FROM Genre WHERE Id=@Id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    genre.Add(new Genre(reader));
                }
                connection.Close();
            }
            return genre[0];
        }

        public Genre Delete(Genre Genre)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"DELETE FROM [dbo].[Genre]
                           WHERE Id=@id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", Genre.Id);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return Genre;
        }

        public Genre Edit(Genre Genre)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"UPDATE Genre
                            SET[GName]=@Name
                            WHERE Id=@id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", Genre.Id);
                cmd.Parameters.AddWithValue("@Name", Genre.Name);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return Genre;
        }
    }
}