using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RentC
{
    class Update_Customer
    {
        public static void UpdateCustomer()
        {
            Console.Clear();
            Console.WriteLine("ClientID:");
            int clientID = int.Parse(Console.ReadLine());
            while (clientID.ToString() == "")
            {
                clientID = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("Client Name:");
            string name = Console.ReadLine();
            while (name == "")
            {
                name = Console.ReadLine();
            }
            Console.WriteLine("Birth Date:");
            string birthDate = Console.ReadLine();
            Console.WriteLine("ZIP:");
            string zip = Console.ReadLine();
            while (zip == "")
            {
                zip = Console.ReadLine();
            }

            string conString = "Data Source=DESKTOP-MJA5A7T;Initial Catalog=Car Rent;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            if (Register_New_Customer.ClientExist(con, clientID) && Register_New_Customer.ZipValidation(zip) && Register_New_Customer.DataValidation(birthDate))
            {
                SqlCommand addCustomer = new SqlCommand("update Customers set Name=@name, BirthDate=@birthDate, Location=@location where CostumerID=@clientID", con);
                addCustomer.Parameters.AddWithValue("@clientId", clientID);
                addCustomer.Parameters.AddWithValue("@name", name);
                addCustomer.Parameters.AddWithValue("@birthDate", birthDate);
                addCustomer.Parameters.AddWithValue("@location", zip);
                SqlDataReader reader = addCustomer.ExecuteReader();
                Program.MenuScreen();
            }
            else
                Console.WriteLine("invalid data");

        }
    }
}
