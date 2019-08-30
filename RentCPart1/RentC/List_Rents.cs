using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace RentC
{
    class List_Rents
    {
        public static void ListRents()
        {
            Console.Clear();
            string conString = "Data Source=DESKTOP-MJA5A7T;Initial Catalog=Car Rent;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select Plate, CostumerID, StartDate, EndDate, Reservations.Location from Reservations join Cars on Reservations.CarID = Cars.CarID", con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            DisplayListRents(dt);
            int column = 0;
            int prevColumn = 0;
            while (true)
            {
                column = int.Parse(Console.ReadLine());
                if (column == prevColumn)
                {
                    switch (column)
                    {
                        case 1:
                            dt = OrderBy.OrderByCustomerIdDesc(dt);
                            break;
                        case 2:
                            dt = OrderBy.OrderByPlateDesc(dt);
                            break;
                        case 3:
                            dt = OrderBy.OrderByStartDateDesc(dt);
                            break;
                        case 4:
                            dt = OrderBy.OrderByEndDateDesc(dt);
                            break;
                        case 5:
                            dt = OrderBy.OrderByLocationDesc(dt);
                            break;
                    }
                }
                else
                {
                    switch (column)
                    {
                        case 1:
                            dt = OrderBy.OrderByCustomerId(dt);
                            break;
                        case 2:
                            dt = OrderBy.OrderByPlate(dt);
                            break;
                        case 3:
                            dt = OrderBy.OrderByStartDate(dt);
                            break;
                        case 4:
                            dt = OrderBy.OrderByEndDate(dt);
                            break;
                        case 5:
                            dt = OrderBy.OrderByLocation(dt);
                            break;
                        default:
                            dt = OrderBy.OrderByCustomerId(dt);
                            break;
                    }
                }
                prevColumn = column;
                DisplayListRents(dt);
            }


        }
         static void DisplayListRents(DataTable dt)
        {
            int width = Console.WindowWidth;
            width = width / 5;
            Write.WriteProgress("Car Plate", 1, 1);
            Write.WriteProgress("ClientID", width, 1);
            Write.WriteProgress("StartDate", 2 * width, 1);
            Write.WriteProgress("StartDate", 3 * width, 1);
            Write.WriteProgress("Location", 4 * width, 1);
            int r = 3;

            foreach (DataRow row in dt.Rows)
            {
                Write.WriteProgress(row["Plate"].ToString(), 1, r);
                Write.WriteProgress(row["CostumerID"].ToString(), width, r);
                Write.WriteProgress(row["StartDate"].ToString(), 2 * width, r);
                Write.WriteProgress(row["EndDate"].ToString(), 3 * width, r);
                Write.WriteProgress(row["Location"].ToString(), 4 * width, r);
                r++;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Sort by:");
            Console.WriteLine("1 - CustumerId");
            Console.WriteLine("2 - Plate");
            Console.WriteLine("3 - StartDate");
            Console.WriteLine("4 - EndDate");
            Console.WriteLine("5 - Location");
        }

       
    }
}
