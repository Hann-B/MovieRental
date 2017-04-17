using MovieRentalShop.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MovieRentalShop.Services
{
    public class MovieServices
    {
        //Establish Connection
        private string _ConnectionString;
        public MovieServices(string connectionString)
        {
            this._ConnectionString = connectionString;
        }

        //GET:List all movies
        public List<Movie> GetAllMovies(bool IncludeGenreData = true)
        {
            var rv = new List<Movie>();
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = "SELECT * FROM Movies";
                if (IncludeGenreData)
                {
                    query += " JOIN Genre ON Genre.Id=GenreId ORDER BY Movies.MName";
                }
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rv.Add(new Movie(reader, true));
                }
                connection.Close();
            }
            return rv;
        }

        //GET:Single Movie based on id
        public Movie GetMovie(int id)
        {
            var movie = new List<Movie>();
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"SELECT * FROM Movies 
                            JOIN Genre ON Genre.Id=GenreId
                            WHERE Movies.Id=@Id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    movie.Add(new Movie(reader));
                }
                connection.Close();
            }
            return movie[0];
        }

        //EDIT Movie
        public Movie Edit(int id)
        {
            var Movie = new Movie();
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"UPDATE Movie
                            SET[MName]=@Name
                            ,[YearReleased]=@YearReleased
                            ,[Director]=@Director
                            ,[GenreId]=@GenreId
                            ,[IsCheckedIn]=@IsCheckedIn
                            WHERE Id=@id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", Movie.Id);
                cmd.Parameters.AddWithValue("@Name", Movie.Name);
                cmd.Parameters.AddWithValue("@Director", Movie.Director);
                cmd.Parameters.AddWithValue("@GenreId", Movie.GenreId);
                cmd.Parameters.AddWithValue("@IsCheckedIn", Movie.IsCheckedIn);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return Movie;
        }

        //RENT MOVIE
        public Movie Rent(Movie Movie, bool IncludeGenre = true)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"UPDATE Movies
                            SET [IsCheckedIn] = @IsCheckedIn
                            WHERE Id=@id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", Movie.Id);
                cmd.Parameters.AddWithValue("@IsCheckedIn", false);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return Movie;
        }

        //RETURN MOVIE
        public Movie Return(Movie Movie, bool IncludeGenre = true)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"UPDATE Movies
                            SET [IsCheckedIn] = @IsCheckedIn
                            WHERE Id=@id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", Movie.Id);
                cmd.Parameters.AddWithValue("@IsCheckedIn", true);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return Movie;
        }

        //Display moives out of store
        public List<Movie> RentedMovies(bool IncludeGenreData = true)
        {
            var rv = new List<Movie>();
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"SELECT *
                              FROM[dbo].[Movies]
                              JOIN Genre ON Genre.Id = GenreId
                              WHERE IsCheckedIn = 0";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rv.Add(new Movie(reader, true));
                }
                connection.Close();
            }
            return rv;
        }
    }
}