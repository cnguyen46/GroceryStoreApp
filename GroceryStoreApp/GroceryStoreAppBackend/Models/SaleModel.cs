using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace GroceryStoreApp.Models
{
    public class SaleModel : ISaleModel
    {
        public SaleModel(int saleID, int productID, decimal percentageOff, DateTime startDate, DateTime endDate)
        {
            SaleID = saleID;
            ProductID = productID;
            PercentageOff = percentageOff;
            StartDate = startDate;
            EndDate = endDate;
        }

        int SaleID { get; set; }
        int ProductID { get; set; }
        decimal PercentageOff { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }


        public bool addSale()
        {
            /*
             *  Calls the database with an insert into with all of these values
             */
            throw new NotImplementedException();
        }

        public JsonResult toJson()
        {
            return new JsonResult(new {SaleID = SaleID,  ProductID = ProductID, PercentageOff = PercentageOff, StartDate = StartDate, EndDate = EndDate});
        }

        public bool getSale()
        {
            /*
             * Calls the database to see if the saleID and the productID match up then if it works then set all of the sale class stuff to what it returns
             */
            string sqlDataSource = Environment.GetEnvironmentVariable("Conn") ?? throw new Exception("Need to create an environmental variable");
            string query = "select * from [Sale] where SaleID = @SaleID and ProductID = @ProductID";

            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SaleID", this.SaleID);
                    command.Parameters.AddWithValue("@ProductID", this.ProductID);
                    reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                SaleID = reader.GetInt32(0);
                                ProductID = reader.GetInt32(1);
                                PercentageOff= reader.GetDecimal(2);
                                StartDate = reader.GetDateTime(3);
                                EndDate = reader.GetDateTime(4);

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
