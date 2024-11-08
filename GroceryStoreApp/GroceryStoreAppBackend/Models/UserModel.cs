using GroceryStoreApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;

namespace GroceryStoreApp.Models
{
    public class UserModel : IUserModel
    {
        public UserModel(int userId, string userName, string password, string email, string firstName, string lastName, long phone)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
        }

        int UserId { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        long Phone { get; set; }


        public bool addUser()
        {
            /*
             * Calls the Database with an Insert into with all of these values
             */

            string sqlDataSource = Environment.GetEnvironmentVariable("Conn") ?? throw new Exception("Need to create an environment variable");
            string query = """
                           INSERT INTO [User] (UserName, FirstName, LastName, Email, Phone, Passcode) 
                            SELECT TOP 1 @username, @firstname, @lastname, @email, @phone, @passcode
                            FROM [User]
                            WHERE NOT EXISTS 
                           	    (SELECT * FROM [User] WHERE UserName = @username AND Email = @email AND Phone = @phone);
                           """;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                SqlCommand command = new SqlCommand(query, connection);
                {
                    command.Parameters.AddWithValue("@username", this.UserName);
                    command.Parameters.AddWithValue("@firstname", this.FirstName);
                    command.Parameters.AddWithValue("@lastname", this.LastName);
                    command.Parameters.AddWithValue("@email", this.Email);
                    command.Parameters.AddWithValue("@phone", this.Phone);
                    command.Parameters.AddWithValue("@passcode", this.Password);
                }
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data inserted successfully.");
                    connection.Close();
                    //This Should work to update the current UserID with what is gotten from the database
                    getUser();
                    CartModel.createCartForNewUser(this.UserId);
                    return true;
                }
                else
                {
                    Console.WriteLine("Fail inserting data.");
                    connection.Close();
                    return false;
                }
            }
            throw new AccessViolationException("Function should not reach here if connection string is correct");
        }


        public JsonResult toJson()
        {
            return new JsonResult(new { UserId = UserId, UserName = UserName, Password = Password, Email = Email, FirstName = FirstName, LastName = LastName, Phone = Phone });
        }

        public bool getUser()
        {
            /*
             * Calls the Database to see if the username and password match up then if it works then set all of the user class stuff to what it returns
             */
            string sqlDataSource = Environment.GetEnvironmentVariable("Conn") ?? throw new Exception("Need to create an environment variable");
            string query = "select * from [User] where UserName = @username and Passcode = @passcode";

            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", this.UserName);
                    command.Parameters.AddWithValue("@passcode", this.Password);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserId = reader.GetInt32(0);
                            UserName = reader.GetString(1);
                            Password = reader.GetString(6);
                            Email = reader.GetString(4);
                            FirstName = reader.GetString(2);
                            LastName = reader.GetString(3);
                            Phone = reader.GetInt64(5);
                            connection.Close();
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            throw new AccessViolationException("Function should not reach here if connection string is correct");
        }


        public bool deleteUser(string userName)
        /*
        * Delete the user in Database
        * Mainly use for deleting the user added into database for NUnit test.
        */
        {
            string sqlDataSource = Environment.GetEnvironmentVariable("Conn") ?? throw new Exception("Need to create an environment variable");
            string query = """
                DELETE FROM [User] WHERE UserName = @username
                """;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", userName);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    Console.WriteLine("Success deleting user.");
                    return true;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Fail deleting user.");
                    Console.WriteLine($"Processing fail: {e.Message}");
                }
            }
            return false;
        }

    }
}
