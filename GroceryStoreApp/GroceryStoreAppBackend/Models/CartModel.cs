using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;

namespace GroceryStoreApp.Models
{
    public class CartModel : ControllerBase, ICartModel
    {
    private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        public CartModel() { }

        static public void createCartForNewUser(int userId)
        {
            string sqlDataSource = Environment.GetEnvironmentVariable("Conn") ?? throw new Exception("Need to create an environment variable");
            string query = "INSERT INTO [Cart] (UserId) VALUES (@userId); ";

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                SqlCommand command = new SqlCommand(query, connection);
                {
                    command.Parameters.AddWithValue("@userId", userId);
                }
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Cart inserted successfully.");
                    connection.Close();
                }
                else
                {
                    Console.WriteLine("Fail inserting data.");
                    connection.Close();
                    throw new Exception("Should Not have gotten here");
                }
            }

        }

        public bool addItemToCart(int productID, int userId)
        {
            try
            {

                // try pulling products for this cart to see if the product is already in it
                string query1 = "select * from CartProduct CP join Cart C on C.CartID = CP.CartID where C.UserID = @UserID and ProductID = @ProductID";
                Dictionary<string, object> parameters1 = new Dictionary<string, object>();
                parameters1.Add("@UserID", userId);
                parameters1.Add("@ProductID", productID);
                DataTable result1 = dbHelper.ExecuteGetQuery(query1, parameters1);

                if (result1.Rows.Count == 0) // product is not yet in the cart, add the cart product
                {
                    string query2 = "insert into CartProduct(CartID, ProductID, Quantity) select CartID, @ProductID, 1 from Cart where UserID = @UserID";
                    Dictionary<string, object> parameters2 = new Dictionary<string, object>();
                    parameters2.Add("@UserID", userId);
                    parameters2.Add("@ProductID", productID);
                    DataTable result2 = dbHelper.ExecuteGetQuery(query2, parameters2);
                }
                else // product is already in the cart, increment the quantity
                {
                    var foundProduct = result1.AsEnumerable().First();
                    int currentQuantity = foundProduct.Field<int>("Quantity");
                    int cartID = foundProduct.Field<int>("CartID");
                    string query3 = "update CartProduct set Quantity = @Quantity where CartID = @CartID and ProductID = @ProductID";
                    Dictionary<string, object> parameters3 = new Dictionary<string, object>();
                    parameters3.Add("@CartID", cartID);
                    parameters3.Add("@ProductID", productID);
                    parameters3.Add("@Quantity", currentQuantity + 1);
                    DataTable result3 = dbHelper.ExecuteGetQuery(query3, parameters3);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }


        public bool removeItemFromCart(int productID, int userId)
        {
            try
            { 

                // pull product from this cart to check its quantity
                string query1 = "select * from CartProduct CP join Cart C on C.CartID = CP.CartID where C.UserID = @UserID and ProductID = @ProductID";
                Dictionary<string, object> parameters1 = new Dictionary<string, object>();
                parameters1.Add("@UserID", userId);
                parameters1.Add("@ProductID", productID);
                DataTable result1 = dbHelper.ExecuteGetQuery(query1, parameters1);

                if (result1.Rows.Count == 0) // product is not in the cart
                {
                    throw new Exception("Product not found in cart.");
                }
                else // product is found
                {
                    var foundProduct = result1.AsEnumerable().First();
                    int currentQuantity = foundProduct.Field<int>("Quantity");
                    int cartID = foundProduct.Field<int>("CartID");

                    if (currentQuantity == 1) // product has quantity of 1, remove the cart product
                    {
                        string query2 = "delete from CartProduct where CartID = @CartID and ProductID = @ProductID";
                        Dictionary<string, object> parameters2 = new Dictionary<string, object>();
                        parameters2.Add("@CartID", cartID);
                        parameters2.Add("@ProductID", productID);
                        DataTable result2 = dbHelper.ExecuteGetQuery(query2, parameters2);
                    }
                    else // product has quantity greater than 1, decrement the quantity
                    {
                        string query3 = "update CartProduct set Quantity = @Quantity where CartID = @CartID and ProductID = @ProductID";
                        Dictionary<string, object> parameters3 = new Dictionary<string, object>();
                        parameters3.Add("@CartID", cartID);
                        parameters3.Add("@ProductID", productID);
                        parameters3.Add("@Quantity", currentQuantity - 1);
                        DataTable result3 = dbHelper.ExecuteGetQuery(query3, parameters3);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public DataTable getItemsFromCart(int userId)
        {

            string sqlDataSource = Environment.GetEnvironmentVariable("Conn") ??
                                   throw new Exception("Need to create an environment variable");
            string query =
                "select * from CartProduct cp join Cart c on cp.CartID = c.CartID join Product p on cp.ProductID = p.ProductID where c.UserId = @UserId ;";
            DataTable table = new DataTable();

            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader myReader = command.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();

                    connection.Close();
                }
            }

            return table;
        }

        public bool checkout(int userId)
        {
            
            string sqlDataSource = Environment.GetEnvironmentVariable("Conn") ?? throw new Exception("Need to create an environment variable");
            string query = """
                        DELETE FROM [CartProduct] WHERE CartID IN (
                            SELECT c.CartID FROM [CartProduct] cp
                            JOIN [Cart] c ON c.CartID = cp.CartID
                            JOIN [User] u ON u.UserID = c.UserID
                            WHERE c.UserID = @userId);
                        """;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                try
                {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                Console.WriteLine("Success deleting cart after cheking out.");
                return true;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Fail deleting cart after cheking cart.");
                    Console.WriteLine($"Processing fail: {e.Message}");
                }
            }
            return false;
        }

        public decimal getPrice(int userId)
        {

            string sqlDataSource = Environment.GetEnvironmentVariable("Conn") ??
                                   throw new Exception("Need to create an environment variable");
            string query =
                "select Price, Quantity from CartProduct cp join Cart c on cp.CartID = c.CartID join Product p on cp.ProductID = p.ProductID where c.UserId = @UserId ;";
            decimal total = 0;

            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            decimal itemPrice = reader.GetDecimal(0);
                            int itemQuantity = reader.GetInt32(1);
                            total += itemPrice * itemQuantity;
                        }
                    }

                    connection.Close();
                }
            }

            return total;
        }
    }
}
