using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Data.Sql;
using System.Text.RegularExpressions;
using Dapper;




namespace RentCMvc.Models
{
    
    public class DatabaseConnection
    {
        public static string ConnectionString()
        {
            return "Data Source=DESKTOP-MJA5A7T;Initial Catalog=Car Rent;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        }
        public static SqlConnection Conecting_to_database()
        {
            string conn = "Data Source=DESKTOP-MJA5A7T;Initial Catalog=Car Rent;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
            SqlConnection connection = new SqlConnection(conn);
            connection.Open();
            SqlCommand c = new SqlCommand();
            c.Connection = connection;
            return connection;
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection con = new SqlConnection(ConnectionString()))
            {
                //IDbCommand command= new SqlCommand
                return con.Query<T>(sql).ToList();
            }
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection con = new SqlConnection(ConnectionString()))
            {
                return con.Execute(sql, data);
            }
        }
    }
}