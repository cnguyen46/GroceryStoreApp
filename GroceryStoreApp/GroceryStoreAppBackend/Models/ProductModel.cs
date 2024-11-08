using System.Data;
using System.Data.SqlClient;

namespace GroceryStoreApp.Models
{
    public class ProductModel : IProductModel
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        public ProductModel(int productId, string sKU, string name, string description, string category, string manufacturer, double price, string image, string size, string weight, decimal rating)
        {
            ProductId = productId;
            SKU = sKU;
            Name = name;
            Description = description;
            Category = category;
            Manufacturer = manufacturer;
            Price = price;
            Image = image;
            Size = size;
            Weight = weight;
            Rating = rating;
        }

        public int ProductId { get; set; }
        string SKU { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Category { get; set; }
        string Manufacturer { get; set; }
        double Price { get; set; }
        string Image { get; set; }
        string Size { get; set; }
        string Weight { get; set; }
        decimal Rating { get; set; }

        /////// Moved this to the DatabaseHelper.cs
        ///
        //private DataTable ExecuteQuery(string query, Dictionary<string, object> parameters)
        //{
        //    DataTable table = new DataTable();

        //    // Need to create an environment variable
        //    string sqlDataSource = Environment.GetEnvironmentVariable("Conn") ?? throw new Exception("Need to create an environment variable");

        //    using (SqlConnection myConn = new SqlConnection(sqlDataSource))
        //    {
        //        myConn.Open();
        //        using (SqlCommand sqlCommand = new SqlCommand(query, myConn))
        //        {
        //            foreach (KeyValuePair<string, object> param in parameters)
        //                sqlCommand.Parameters.AddWithValue(param.Key, param.Value); // Add parameter for category

        //            SqlDataReader myReader = sqlCommand.ExecuteReader();
        //            table.Load(myReader);
        //            myReader.Close();
        //        }
        //        myConn.Close();
        //    }
        //    return table;
        //}

        public DataTable getProductsFromDatabase()
        {
            string query = "select * from Product";
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            return dbHelper.ExecuteGetQuery(query, parameters);
        }

        public DataTable getProductsFromDatabaseByCategory()
        {
            string query = "SELECT * FROM Product WHERE Category = @Category";
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("@Category", this.Category);

            return dbHelper.ExecuteGetQuery(query, parameters);
        }

        public DataTable getProductsFromDatabaseById()
        {
            string query = "SELECT * FROM Product WHERE ProductID = @ProductID";
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("@ProductID", this.ProductId);

            return dbHelper.ExecuteGetQuery(query, parameters);
        }
    }
}
