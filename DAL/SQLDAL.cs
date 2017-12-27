using System;
using System.Data.SqlClient;

namespace LugggeTracker.DAL
{
    public class SQLDAL
    {

        string SqlConnectionString = "integrated security=SSPI;Database=Master;Server=."; //TODO read connection string from config

        public void AddPassenger(string passengerFirstName, 
                                    string passengerMiddleName,
                                    string passengerLastName,
                                    )
        {
            try
            {
                using (SqlConnection SqlConn = new SqlConnection(SqlConnectionString))
                {
                    SqlConn.Open();
                    string SqlCommand = String.Format(System.Globalization.CultureInfo.InvariantCulture,
                        "Insert into Passengers values('{0}','{1}')", phoneNo, name);

                    using (SqlCommand SqlCmd = new SqlCommand(SqlCommand, SqlConn))
                    {
                        SqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
