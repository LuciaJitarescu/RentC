using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace RentCMvc.Models
{
    public class Update_Customer
    {
        public static void Update_Client(string city, string name, string date, string id, SqlConnection connection)
        {
            using (SqlCommand com = new SqlCommand("update Customers set  Name='" + name + "', BirthDate='" + date + "', Location='" + city + "' where CostumerID= " + id, connection))
            {
                com.ExecuteNonQuery();
            }
        }
    }
}