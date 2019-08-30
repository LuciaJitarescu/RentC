using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace RentCMvc.Models
{
    public class CountRentsForCustomer
    {
        public static int CountRents(int CustomerID)
        {
            int count = 0;
            SqlConnection con = DatabaseConnection.Conecting_to_database();
            var data = DatabaseConnection.LoadData<Rents>("select CostumerID from Reservations");
            foreach (var i in data)
            {
                if (i.CostumerID == CustomerID)
                    count++;
            }
            return count;
        }
        private static List<CustomerRents> CheckForDuplicateItems(List<CustomerRents> items)
        {
            foreach (var i in items)
            {

                int count = 0;
                foreach (var t in items)
                {
                    if (t.CustomerID == i.CustomerID)
                        count++;
                }
                if (count > 1)
                    items.Remove(i);
            }
            return items;
        }
    }

}