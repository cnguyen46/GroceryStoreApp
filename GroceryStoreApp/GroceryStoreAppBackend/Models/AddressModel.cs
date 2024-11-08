using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace GroceryStoreApp.Models
{
    public class AddressModel : IAddressModel
    {
        public AddressModel(int addressID, int userID, string streetAddress, string state, string city, int zip)
        {
            AddressID = addressID;
            UserID = userID;
            StreetAddress = streetAddress;
            State = state;
            City = city;
            Zip = zip;
        }

        int AddressID { get; set; }
        int UserID { get; set; }
        string StreetAddress { get; set; }
        string State { get; set; }
        string City { get; set; }
        int Zip { get; set; }



        public bool addAddress()
        {
            /*
             * Calls the databse with an insert into with all of these values
             */

            string sqlDataSource = Environment.GetEnvironmentVariable("Conn") ?? throw new Exception("Need to create an environment variable");
            string query = """
                           INSERT INTO [Address] (UserId, StreetAddress, State, City, Zip) 
                            SELECT TOP 1 @userId, @street, @state, @city, @zip
                            FROM [Address]
                            WHERE NOT EXISTS 
                                (SELECT * FROM [Address] WHERE UserId = @userId 
                                    AND StreetAddress = @street 
                                    AND State = @state 
                                    AND City = @city 
                                    AND Zip = @zip);
                           """;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                SqlCommand command = new SqlCommand(query, connection);
                {
                    command.Parameters.AddWithValue("@userID", this.UserID);
                    command.Parameters.AddWithValue("@street", this.StreetAddress);
                    command.Parameters.AddWithValue("@state", this.State);
                    command.Parameters.AddWithValue("@city", this.City);
                    command.Parameters.AddWithValue("@zip", this.Zip);
                }
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data inserted successfully.");
                    connection.Close();
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
            return new JsonResult(new {AddressID = AddressID, UserID = UserID, StreetAddress = StreetAddress, State = State, City = City, Zip = Zip});
        }

        public bool getAddress()
        {
            /*
             * Calls the database to see if userID and address details match up, then if it works then set all of address class to what it returns
             */
            string sqlDataSource = Environment.GetEnvironmentVariable("Conn") ?? throw new Exception("Need to create an environmental variable");
            string query = "select * from [Address] where UserID = @UserID and StreetAddress = @StreetAddress";

            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", this.UserID);
                    command.Parameters.AddWithValue("@StreetAddress", this.StreetAddress);
                    reader = command.ExecuteReader();
                        if(reader.HasRows)
                        { 
                            while (reader.Read())
                            {
                                AddressID = reader.GetInt32(0);
                                UserID = reader.GetInt32(1);
                                StreetAddress = reader.GetString(2);
                                State = reader.GetString(3);
                                City = reader.GetString(4);
                                Zip = reader.GetInt32(5);

                                return true;
                            }
                        }
                        else
                        {
                            return false;
                        }
                }
                connection.Close();
            }
            throw new AccessViolationException("Function should not reach here if connection string is correct");
        }
    }
}

