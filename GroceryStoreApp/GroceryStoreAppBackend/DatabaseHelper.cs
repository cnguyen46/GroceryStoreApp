using System.Data;
using System.Data.SqlClient;

namespace GroceryStoreApp
{
	public class DatabaseHelper
	{
		public DatabaseHelper()
		{

		}

		public DataTable ExecuteGetQuery(string query, Dictionary<string, object> parameters)
		{
			DataTable table = new DataTable();

			// Need to create an environment variable
			string sqlDataSource = Environment.GetEnvironmentVariable("Conn") ?? throw new Exception("Need to create an environment variable");

			using (SqlConnection myConn = new SqlConnection(sqlDataSource))
			{
				myConn.Open();
				using (SqlCommand sqlCommand = new SqlCommand(query, myConn))
				{
					foreach (KeyValuePair<string, object> param in parameters)
						sqlCommand.Parameters.AddWithValue(param.Key, param.Value); // Add parameter for category

					SqlDataReader myReader = sqlCommand.ExecuteReader();
					table.Load(myReader);
					myReader.Close();
				}
				myConn.Close();
			}
			return table;
		}

		public int ExecutePostQuery(string query, Dictionary<string, object> parameters)
		{
			int result = 0;

            // Need to create an environment variable
            string sqlDataSource = Environment.GetEnvironmentVariable("Conn") ?? throw new Exception("Need to create an environment variable");

			using (SqlConnection myConn = new SqlConnection(sqlDataSource))
			{
				myConn.Open();
				using (SqlCommand sqlCommand = new SqlCommand(query, myConn))
				{
					foreach (KeyValuePair<string, object> param in parameters)
						sqlCommand.Parameters.AddWithValue(param.Key, param.Value); // Add parameter for category

					result = sqlCommand.ExecuteNonQuery();
				}
				myConn.Close();
			}
			return result;
		}
	}
}
