using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RentC
{
    class List_Customers
    {
        public static void ListCustomers()
        {
            Console.Clear();
            string conString = "Data Source=DESKTOP-MJA5A7T;Initial Catalog=Car Rent;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from Customers", con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            DisplayListCustomers(dt);
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
                            dt = OrderBy.OrderByNameDesc(dt);
                            break;
                        case 3:
                            dt = OrderBy.OrderByBirthDateDesc(dt);
                            break;
                        case 4:
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
                            dt = OrderBy.OrderByName(dt);
                            break;
                        case 3:
                            dt = OrderBy.OrderByBirthDate(dt);
                            break;
                        case 4:
                            dt = OrderBy.OrderByLocation(dt);
                            break;
                        default:
                            dt = OrderBy.OrderByCustomerId(dt);
                            break;
                    }
                }
                prevColumn = column;
                DisplayListCustomers(dt);
            }

        }
        static void DisplayListCustomers(DataTable dt)
        {
            Console.Clear();
            int width = Console.WindowWidth;
            width = width / 4;
            Write.WriteProgress("CustomerID", 1, 1);
            Write.WriteProgress("Name", width, 1);
            Write.WriteProgress("BirthDate", 2 * width, 1);
            Write.WriteProgress("Location", 3 * width, 1);
            int r = 3;

            foreach (DataRow row in dt.Rows)
            {
                Write.WriteProgress(row["CostumerID"].ToString(), 1, r);
                Write.WriteProgress(row["Name"].ToString(), width, r);
                Write.WriteProgress(row["BirthDate"].ToString(), 2 * width, r);
                Write.WriteProgress(row["Location"].ToString(), 3 * width, r);
                r++;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Sort by:");
            Console.WriteLine("1 - CustomerId");
            Console.WriteLine("2 - Name");
            Console.WriteLine("3 - BirthDate");
            Console.WriteLine("4 - Location");
            Console.WriteLine();
        }
    }
}
