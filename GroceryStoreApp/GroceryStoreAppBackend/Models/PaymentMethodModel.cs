using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace GroceryStoreApp.Models
{
    public class PaymentMethodModel : IPaymentMethodModel
    {
        public PaymentMethodModel(int paymentMethodID, int userID, string nameOnCard, string cardAccountNumber, DateTime cardExpiration, int securityCode)
        {
            PaymentMethodID = paymentMethodID;
            UserID = userID;
            NameOnCard = nameOnCard;
            CardAccountNumber = cardAccountNumber;
            CardExpiration = cardExpiration;
            SecurityCode = securityCode; 
        }

        int PaymentMethodID { get; set; }
        int UserID { get; set; }
        string NameOnCard { get; set; }
        string CardAccountNumber { get; set; }
        DateTime CardExpiration { get; set; }
        int SecurityCode { get; set; }


        public bool addPaymentMethod()
        {
            /*
             *  Calls the Database with an insert into with all of these values
             */
            throw new NotImplementedException();
        }

        public JsonResult toJson()
        {
            return new JsonResult(new {PaymentMethodID = PaymentMethodID, UserID = UserID, NameOnCard = NameOnCard, CardAccountNumber = CardAccountNumber, CardExpiration = CardExpiration, SecurityCode = SecurityCode});
        }

        public bool getPaymentMethod()
        {
            /*
             *  calls the database to see if the payment method ID and the user ID match up then if it works then set all of the payment method class stuff to what it returns
             */
            string sqlDataSource = Environment.GetEnvironmentVariable("Conn") ?? throw new Exception("Need to create an environmental variable");
            string query = "select * from [PaymentMethod] where PaymentMethodID = @PaymentMethodID and UserID = @UserID";

            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PaymentMethodID", this.PaymentMethodID);
                    command.Parameters.AddWithValue("@UserID", this.UserID);
                    reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PaymentMethodID = reader.GetInt32(0);
                                UserID = reader.GetInt32(1);
                                NameOnCard = reader.GetString(2);
                                CardAccountNumber = reader.GetString(3);
                                CardExpiration = reader.GetDateTime(4);
                                SecurityCode = reader.GetInt32(5);

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
