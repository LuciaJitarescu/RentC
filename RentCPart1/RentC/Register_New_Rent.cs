using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace RentC
{
    class Register_New_Rent
    {
        public static void RegisterNewCarRent()
        {
            Console.Clear();
            //reads until a value is entered
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
                if (!(IntValidation(clientId)))
                    Console.WriteLine("Invalid data");
            }
            Console.WriteLine("Start Date:");
            string startDate = "";
            while (startDate == "")
            {
                startDate = Console.ReadLine();
                if (!(DataValidation(startDate)))
                    Console.WriteLine("Invalid data");
            }
            Console.WriteLine("End Date:");
            string endDate = "";
            while (endDate == "")
            {
                endDate = Console.ReadLine();
                if (!(DataValidation(endDate)))
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

            //get the carId
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
            //register new rent
            if (CarAvailable(con, carId, DateTime.Parse(startDate), DateTime.Parse(endDate)) && ClientExist(con, int.Parse(clientId)) && CityAvailable(con, int.Parse(clientId), plate) && CheckDate(DateTime.Parse(startDate), DateTime.Parse(endDate)))
            {

                string register = "insert into Reservations(CarID, CostumerID, ReservStatsID, StartDate, EndDate, Location, CouponCode) values (@carID, @customerID, 1, @StartDate, @EndDate, @Location, @coupon)";
                SqlCommand registerNewRent = new SqlCommand(register, con);
                registerNewRent.Parameters.AddWithValue("@carID", int.Parse(carId));
                registerNewRent.Parameters.AddWithValue("@customerId", int.Parse(clientId));
                registerNewRent.Parameters.AddWithValue("@StartDate", DateTime.Parse(startDate));
                registerNewRent.Parameters.AddWithValue("@EndDate", DateTime.Parse(endDate));
                registerNewRent.Parameters.AddWithValue("@Location", city);
                registerNewRent.Parameters.AddWithValue("@coupon", Randome_cupon(con));

                SqlDataReader reader = registerNewRent.ExecuteReader();
                Console.WriteLine("New car rent added");
                Program.MenuScreen();
            }
            else
                Console.WriteLine("Invalid data");
            con.Close();
        }

        public static bool CityAvailable(SqlConnection con, int clientId, string plate)
        {
            if (ClientLocation(con, clientId) == CarLocation(con, plate))
                return true;
            else return false;
        }

        public static bool CheckDate(DateTime startDate, DateTime endDate)
        {
            if (endDate >= startDate)
                return true;
            else
                return false;
        }
        //get the car location
       public  static string CarLocation(SqlConnection con, string plate)
        {
            SqlCommand carLocation = new SqlCommand("select Location from Cars where Plate=@Plate", con);
            carLocation.Parameters.AddWithValue("@Plate", plate);
            SqlDataReader rCarLocation = carLocation.ExecuteReader();
            string crLocation = "";
            while (rCarLocation.Read())
            {
                crLocation = rCarLocation.GetValue(0).ToString();
            }
            rCarLocation.Close();
            return crLocation;
        }
        //get the client location
        public static string ClientLocation(SqlConnection con, int clientId)
        {
            string cLocation = "select Location from Customers where Customers.CostumerID=@ClientID";
            SqlCommand clientLocation = new SqlCommand(cLocation, con);
            clientLocation.Parameters.AddWithValue("@ClientID", clientId);
            SqlDataReader rClientLocation = clientLocation.ExecuteReader();
            string clLocation = "";
            while (rClientLocation.Read())
            {
                clLocation = rClientLocation.GetValue(0).ToString();
            }
            rClientLocation.Close();
            return clLocation;
        }
        //check if car exists and is available for the custumer
        public static bool CarAvailable(SqlConnection con, string carID, DateTime startDate, DateTime endDate)
        {

            SqlDataAdapter carAvailable = new SqlDataAdapter("select StartDate, EndDate,Name from Reservations join ReservationStatuses on Reservations.ReservStatsID=ReservationStatuses.ReservStatsID where CarID=" + carID, con);
            DataTable dt = new DataTable();
            carAvailable.Fill(dt);
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                
                foreach (DataRow row in dt.Rows)
                {
                    DateTime start = DateTime.Parse(row["StartDate"].ToString());
                    DateTime end = DateTime.Parse(row["EndDate"].ToString());
                    string name = row["Name"].ToString();
                    if ((date == start || date == end || (date > start && date < end)) && name == "OPEN")
                        return false;
                }
            }
            return true;
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
        public static bool IntValidation(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (!(Char.IsDigit(s[i])))
                    return false;
            }
            return true;
        }
        public static string Randome_cupon(SqlConnection con)
        {
            Random rnd = new Random();
            int rand = rnd.Next(1, 51);
            string cupon = "1";
            using (SqlCommand sqlCommand = new SqlCommand("SELECT  * from Coupons ", con))
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read() & rand != 0)
                {
                    cupon = reader[0].ToString();
                    rand--;
                }
                reader.Close();
            }
            return cupon;
        }

    }
}
