using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;

namespace RentCMvc.Models
{
    public class RegisterCustomer
    {
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
                    //Console.WriteLine(clientId + " exista");
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

        public static void Register_New_Customer(string city, string name, string date, string id, SqlConnection connection)
        {
            using (SqlCommand sqlCommand = new SqlCommand("SET IDENTITY_INSERT Customers ON;insert into Customers (CostumerID,Name, BirthDate, Location) values ('" + id + "','" + name + "','" + date + "','" + city + "');SET IDENTITY_INSERT Customers OFF", connection))
            {
                sqlCommand.Parameters.AddWithValue("CostumerID", id);
                sqlCommand.Parameters.AddWithValue("Name", name);
                sqlCommand.Parameters.AddWithValue("BirthDate", date);
                sqlCommand.Parameters.AddWithValue("Location", city);
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}