using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace RentCMvc.Models
{
    public class RegisterNewRent
    {
        public static bool DataValidation(string date)
        {
            DateTime dt;
            bool converted = DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            return converted;

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
       
        public static void Register_car_rent(string CarId, string ClientID, string ReservationStatus, string start, string end, string city, string cupon, SqlConnection connection)
        {
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Reservations (CarID , CostumerID,ReservStatsID,StartDate,EndDate,Location,CouponCode) VALUES  ('" + CarId + "','" + ClientID + "','" + 1 + "','" + start + "','" + end + "','" + city + "','" + cupon + "')", connection))
            {
                sqlCommand.Parameters.AddWithValue("@CostumerId", ClientID);
                sqlCommand.Parameters.AddWithValue("@CarId", CarId);
                sqlCommand.Parameters.AddWithValue("@ReservStatsID", ReservationStatus);
                sqlCommand.Parameters.AddWithValue("@StartDate", start);
                sqlCommand.Parameters.AddWithValue("@EndDate", end);
                sqlCommand.Parameters.AddWithValue("@Location", city);
                sqlCommand.Parameters.AddWithValue("@CouponCode", cupon);
                sqlCommand.ExecuteNonQuery();
            }
        }
        public static string Randome_cupon(SqlConnection connection)
        {
            Random rnd = new Random();
            int rand = rnd.Next(1, 51);
            string cupon = "1";
            using (SqlCommand sqlCommand = new SqlCommand("SELECT  * from Coupons ", connection))
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
        public static string CarID_from_plate(string plate, SqlConnection connection)
        {
            string ToReturn = "";
            using (SqlCommand sqlCommand = new SqlCommand("SELECT  * from Cars where Cars.Plate = '" + plate + "'", connection))
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    ToReturn = reader[0].ToString();
                }
                reader.Close();
            }
            return ToReturn;
        }
       
        public static bool CheckDate(DateTime startDate, DateTime endDate)
        {
            if (endDate >= startDate)
                return true;
            else
                return false;
        }

    }
}