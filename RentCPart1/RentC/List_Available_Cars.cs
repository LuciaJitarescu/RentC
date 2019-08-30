using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RentC
{
    class List_Available_Cars
    {
        public static void ListAvailableCars()
        {
            Console.Clear();
            string conString = "Data Source=DESKTOP-MJA5A7T;Initial Catalog=Car Rent;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select Cars.CarID,Plate, Model, Manufacturer, PricePerDay, Reservations.Location  from Cars join Reservations on Cars.CarID=Reservations.CarID", con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            Console.WriteLine("Start date:");
            string startDate = "";
            while (startDate == "")
            {
                startDate = Console.ReadLine();
                if (!(Register_New_Rent.DataValidation(startDate)))
                    Console.WriteLine("Invalid data");
            }
            Console.WriteLine("End Date:");
            string endDate = "";
            while (endDate == "")
            {
                endDate = Console.ReadLine();
                if (!(Register_New_Rent.DataValidation(endDate)))
                    Console.WriteLine("Invalid data");
            }
            DisplayListAvailableCars(dt, startDate, endDate, con);
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
                            dt = OrderBy.OrderByCarIDDesc(dt);
                            break;
                        case 2:
                            dt = OrderBy.OrderByPlateDesc(dt);
                            break;
                        case 3:
                            dt = OrderBy.OrderByManufacturerDesc(dt);
                            break;
                        case 4:
                            dt = OrderBy.OrderByModelDesc(dt);
                            break;
                        case 5:
                            dt = OrderBy.OrderByPricePerDayDesc(dt);
                            break;
                        case 6:
                            dt = OrderBy.OrderByLocationDesc(dt);
                            break;
                    }
                }
                else
                {
                    switch (column)
                    {
                        case 1:
                            dt = OrderBy.OrderByCarID(dt);
                            break;
                        case 2:
                            dt = OrderBy.OrderByPlate(dt);
                            break;
                        case 3:
                            dt = OrderBy.OrderByManufacturer(dt);
                            break;
                        case 4:
                            dt = OrderBy.OrderByModel(dt);
                            break;
                        case 5:
                            dt = OrderBy.OrderByPricePerDay(dt);
                            break;
                        case 6:
                            dt = OrderBy.OrderByLocation(dt);
                            break;
                        default:
                            dt = OrderBy.OrderByCarID(dt);
                            break;
                    }
                }
                prevColumn = column;
                DisplayListAvailableCars(dt, startDate, endDate, con);

            }
        }
        static void DisplayListAvailableCars(DataTable dt, string startDate, string endDate, SqlConnection con)
        {
            Console.Clear();

            int width = Console.WindowWidth;
            width = width / 6;
            Write.WriteProgress("CarID", 1, 1);
            Write.WriteProgress("Plate", width, 1);
            Write.WriteProgress("Manufacturer", 2 * width, 1);
            Write.WriteProgress("Model", 3 * width, 1);
            Write.WriteProgress("Price Per Day", 4 * width, 1);
            Write.WriteProgress("Location", 5 * width, 1);
            int r = 3;

            foreach (DataRow row in dt.Rows)
            {
                int carId = int.Parse(row["CarID"].ToString());
                if (Register_New_Rent.CarAvailable(con, carId.ToString(), DateTime.Parse(startDate), DateTime.Parse(endDate)))
                {
                    Write.WriteProgress(row["CarID"].ToString(), 1, r);
                    Write.WriteProgress(row["Plate"].ToString(), width, r);
                    Write.WriteProgress(row["Manufacturer"].ToString(), 2 * width, r);
                    Write.WriteProgress(row["Model"].ToString(), 3 * width, r);
                    Write.WriteProgress(row["PricePerDay"].ToString(), 4 * width, r);
                    Write.WriteProgress(row["Location"].ToString(), 5 * width, r);
                    r++;
                }

            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Sort by:");
            Console.WriteLine("1 - CarID");
            Console.WriteLine("2 - Plate");
            Console.WriteLine("3 - Manufacturer");
            Console.WriteLine("4 - Model");
            Console.WriteLine("5 - Price Per Day");
            Console.WriteLine("6 - Location");
        }
    }
}
