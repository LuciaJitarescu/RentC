using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace RentCMvc.Models
{
    public class Update_Rent
    {
        public static void update_rent(SqlConnection connection, string clientID, string carID, string start, string end, string city)
        {
            using (SqlCommand com = new SqlCommand("update Reservations set CarID='" + carID + "',StartDate='" + start + "',EndDate='" + end + "', Location='" + city + "' where CostumerID= " + clientID, connection))
            {
                com.ExecuteNonQuery();
            }
        }
        
    }
}