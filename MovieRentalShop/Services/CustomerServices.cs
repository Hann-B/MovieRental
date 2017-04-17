using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieRentalShop.Models;
using System.Data.SqlClient;

namespace MovieRentalShop.Services
{
    public class CustomerServices
    {
        private string _ConnectionString;
        public CustomerServices(string connectionString)
        {
            this._ConnectionString = connectionString;
        }
        public List<Customer> GetAllCustomers()
        {
            var rv = new List<Customer>();
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = "SELECT * FROM Customer";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rv.Add(new Customer(reader));
                }
                connection.Close();
            }
            return rv;
        }
        public Customer GetCustomer(int id)
        {
            var customer = new List<Customer>();
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"SELECT * FROM Customer WHERE Id=@Id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customer.Add(new Customer(reader));
                }
                connection.Close();
            }
            return customer[0];
        }
        public Customer Add(Customer Customer)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"INSERT INTO Customer
                            ([Name]
                            ,[Email]
                            ,[PhoneNumber])
                            VALUES(@Name, @Email, @PhoneNumber)";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Name", Customer.Name);
                cmd.Parameters.AddWithValue("@Email", Customer.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", Customer.PhoneNumber);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return Customer;
        }
        public Customer Delete(Customer Customer)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"DELETE FROM [dbo].[Customer]
                           WHERE Id=@id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", Customer.Id);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return Customer;
        }
        public Customer Edit(Customer Customer)
        {
            using(var connection = new SqlConnection(_ConnectionString))
            {
                var query = @"UPDATE Customer
                            SET[Name]=@Name
                            ,[Email]=@Email
                            ,[PhoneNumber]=@PhoneNumber
                            WHERE Id=@id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", Customer.Id);
                cmd.Parameters.AddWithValue("@Name", Customer.Name);
                cmd.Parameters.AddWithValue("@Email", Customer.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", Customer.PhoneNumber);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return Customer;
        }
       
    }
}