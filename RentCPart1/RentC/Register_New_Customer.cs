using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RentC
{
    class Register_New_Customer
    {
        public static void RegisterNewCustomer()
        {
            Console.Clear();
            try
            {
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

                string conString = "Data Source=DESKTOP-MJA5A7T;Initial Catalog=Car Rent;Integrated Security=True";
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                if (!ClientExist(con, clientID) && ZipValidation(zip) && DataValidation(birthDate))
                {
                    SqlCommand addCustomer = new SqlCommand("set identity_insert Customers ON insert into Customers(CostumerID, Name, BirthDate, Location) values(@clientID,@name,@birthDate, @location) set IDENTITY_INSERT Customers off", con);
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
            catch (FormatException)
            {
                Console.WriteLine("Invalid data");
            }

        }
        public static bool ClientExist(SqlConnection con, int clientId)
        {
            string clientE = "select CostumerID from Customers";
            SqlCommand clientExists = new SqlCommand(clientE, con);
            SqlDataReader rClientExists = clientExists.ExecuteReader();

            while (rClientExists.Read())
            {
                int client = int.Parse(rClientExists.GetValue(0).ToString());
                if (clientId == client)
                {
                    rClientExists.Close();
                    return true;
                }
            }
            rClientExists.Close();
            return false;
        }
        public static bool DataValidation(string date)
        {
            DateTime dt;
            bool converted = DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            return converted;

        }
        public static bool ZipValidation(string zip)
        {
            if (!Regex.Match(zip, @"^\d{5}$").Success)
            {
                return false;
            }
            return true;
        }
    }
}
