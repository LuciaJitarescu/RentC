using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace RentCMvc.Models
{
    public class CountsOfRentsForEveryCar
    {
        public static int NOfRents(int CarID)
        {
            SqlConnection con = DatabaseConnection.Conecting_to_database();
            int count = 0;
            var data = DatabaseConnection.LoadData<Rents>("select * from Reservations");
            foreach(var i in data)
            {
                if (i.CarID == CarID)
                    count++;
            }
            return count;
        }
       
    }
}