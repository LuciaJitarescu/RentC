using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace RentC
{
    class Most_Less_Rented_Cars
    {
        public static void MostRentedCars()
        {
            Console.Clear();
            string conString = "Data Source=DESKTOP-MJA5A7T;Initial Catalog=Car Rent;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            Console.WriteLine("Month:");
            
            int month = int.Parse(Console.ReadLine());
            Console.WriteLine("Year");
            int year = int.Parse(Console.ReadLine());
            Console.Clear();
            //add into the table only the cars rented in the desired month
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select r.CarID, Plate from Reservations r join Cars c on r.CarID=c.CarID where MONTH(StartDate)=@month and YEAR(StartDate)=@year", con);
            dataAdapter.SelectCommand.Parameters.AddWithValue("@month", month);
            dataAdapter.SelectCommand.Parameters.AddWithValue("@year", year);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            //create a new table with an extra column- counts of rents 
            DataTable newTable = NewTableWithCountRentsColumn(dt);
            //sort the table
            newTable = OrderBy.OrderByNumberOfRentsDesc(newTable);
            int width = Console.WindowWidth;
            width = width / 3;
            Write.WriteProgress("CarID", 1, 1);
            Write.WriteProgress("Plate", width, 1);
            Write.WriteProgress("NumberOfRents", 2 * width, 1);

            int r = 3;
            foreach (DataRow row in newTable.Rows)
            {
                Write.WriteProgress(row["CarID"].ToString(), 1, r);
                Write.WriteProgress(row["Plate"].ToString(), width, r);
                Write.WriteProgress(row["NumberOfRents"].ToString(), 2 * width, r);
                r++;
                if (r == 26)
                    break;
            }


        }
        public static void LessRentedCars()
        {
            Console.Clear();
            string conString = "Data Source=DESKTOP-MJA5A7T;Initial Catalog=Car Rent;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            Console.WriteLine("Month:");
            int month = int.Parse(Console.ReadLine());
            Console.WriteLine("Year");
            int year = int.Parse(Console.ReadLine());
            Console.Clear();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select r.CarID, Plate from Reservations r join Cars c on r.CarID=c.CarID where MONTH(StartDate)=@month and YEAR(StartDate)=@year", con);
            dataAdapter.SelectCommand.Parameters.AddWithValue("@month", month);
            dataAdapter.SelectCommand.Parameters.AddWithValue("@year", year);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            DataTable newTable = NewTableWithCountRentsColumn(dt);
            newTable = OrderBy.OrderByNumberOfRents(newTable);
            int width = Console.WindowWidth;
            width = width / 3;
            Write.WriteProgress("CarID", 1, 1);
            Write.WriteProgress("Plate", width, 1);
           Write.WriteProgress("NumberOfRents", 2 * width, 1);

            int r = 3;
            foreach (DataRow row in newTable.Rows)
            {
                Write.WriteProgress(row["CarID"].ToString(), 1, r);
                Write.WriteProgress(row["Plate"].ToString(), width, r);
                Write.WriteProgress(row["NumberOfRents"].ToString(), 2 * width, r);
                r++;
                if (r == 26)
                    break;
            }

        }
        static DataTable NewTableWithCountRentsColumn(DataTable dt)
        {
            DataTable newTable = new DataTable();
            newTable.Columns.Add("CarID");
            newTable.Columns.Add("Plate");
            newTable.Columns.Add("NumberOfRents");
            foreach (DataRow row in dt.Rows)
            {
                int count = CountRentsForCar(row["CarID"].ToString(), dt);
                DataRow newRow = newTable.NewRow();
                newRow["CarID"] = row["CarID"].ToString();
                newRow["Plate"] = row["Plate"].ToString();
                newRow["NumberOfRents"] = count;
                newTable.Rows.Add(newRow);
            }
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();
            //remove duplicates
            foreach (DataRow drow in newTable.Rows)
            {
                if (hTable.Contains(drow["CarID"]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow["CarID"], string.Empty);
            }

            foreach (DataRow dRow in duplicateList)
                newTable.Rows.Remove(dRow);
            return newTable;
        }
        static int CountRentsForCar(string carID, DataTable dt)
        {
            int count = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (row["CarID"].ToString() == carID)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
