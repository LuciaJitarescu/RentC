using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RentC
{
    class Update_Rent
    {
        public static void UpdateCarRent()
        {
            Console.Clear();

            Console.WriteLine("Cart Plate:");
            string plate = "";
            while (plate == "")
            {
                plate = Console.ReadLine();
            }
            Console.WriteLine("Client ID:");
            string clientId = "";
            while (clientId == "")
            {
                clientId = Console.ReadLine();
                if (!(Register_New_Rent.IntValidation(clientId)))
                    Console.WriteLine("Invalid data");
            }
            Console.WriteLine("Start Date:");
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
            Console.WriteLine("City:");
            string city = Console.ReadLine();
            while (city == "")
            {
                city = Console.ReadLine();
            }

            string conString = "Data Source=DESKTOP-MJA5A7T;Initial Catalog=Car Rent;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();


            SqlCommand carID = new SqlCommand("select CarID from Cars where Plate = @CarPlate", con);
            carID.Parameters.AddWithValue("@CarPlate", plate);
            SqlDataReader rCarID = carID.ExecuteReader();
            string carId = "";
            while (rCarID.Read())
            {
                carId = rCarID.GetValue(0).ToString();
                Console.WriteLine(carId);
            }
            rCarID.Close();
            if (Register_New_Rent.CarAvailable(con, carId, DateTime.Parse(startDate), DateTime.Parse(endDate)) && Register_New_Rent.ClientExist(con, int.Parse(clientId)) && Register_New_Rent.CityAvailable(con, int.Parse(clientId), plate) && Register_New_Rent.CheckDate(DateTime.Parse(startDate), DateTime.Parse(endDate)))
            {

                string register = "update Reservations set StartDate=@StartDate, EndDate=@EndDate, Location=@Location  where CarID=@carID and CostumerID=@customerID";
                SqlCommand registerNewRent = new SqlCommand(register, con);
                registerNewRent.Parameters.AddWithValue("@carID", int.Parse(carId));
                registerNewRent.Parameters.AddWithValue("@customerId", int.Parse(clientId));
                registerNewRent.Parameters.AddWithValue("@StartDate", DateTime.Parse(startDate));
                registerNewRent.Parameters.AddWithValue("@EndDate", DateTime.Parse(endDate));
                registerNewRent.Parameters.AddWithValue("@Location", city);
                SqlDataReader reader = registerNewRent.ExecuteReader();
                Program.MenuScreen();
            }
            else
                Console.WriteLine("Invalid data");
            con.Close();
        }
    }
}
