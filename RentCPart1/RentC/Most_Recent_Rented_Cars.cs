using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RentC
{
    class Most_Recent_Rented_Cars
    {
       public static void MostRecentRentedCars()
        {
            Console.Clear();
            string conString = "Data Source=DESKTOP-MJA5A7T;Initial Catalog=Car Rent;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from Reservations", con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            DataTable newTable = new DataTable();
            newTable.Columns.Add("CarID");
            newTable.Columns.Add("StartDate");
            newTable.Columns.Add("EndDate");
            newTable.Columns.Add("Location");
            //order by endDate
            dt = OrderBy.OrderByEndDateDesc(dt);
            //add into the tabel just the days from the past
            foreach (DataRow row in dt.Rows)
            {
                if (DateTime.Parse(row["EndDate"].ToString()) < DateTime.Now)
                {
                    newTable.ImportRow(row);
                }
            }
           
            int width = Console.WindowWidth;
            width = width / 4;
            Write.WriteProgress("CarID", 1, 1);
            Write.WriteProgress("StartDate", width, 1);
            Write.WriteProgress("EndDate", 2 * width, 1);
            Write.WriteProgress("Location", 3 * width, 1);
            int r = 3;
            foreach (DataRow row in newTable.Rows)
            {
                Write.WriteProgress(row["CarID"].ToString(), 1, r);
                Write.WriteProgress(row["StartDate"].ToString(), width, r);
                Write.WriteProgress(row["EndDate"].ToString(), 2 * width, r);
                Write.WriteProgress(row["Location"].ToString(), 3 * width, r);
                r++;
                //display just the first cars(20) from the table
                if (r == 23)
                    break;
            }
        }
    }
}
