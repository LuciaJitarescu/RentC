using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace RentCMvc.Models
{
    public class UsersAndPermissions
    {
        public static bool UserIsSalesperson(string UserId, SqlConnection con)
        {
            int a = Convert.ToInt32(UserId);
            using (SqlCommand command = new SqlCommand("select name from Roles r join Users u on r.RoleID=u.RoleID where UserID='" + a + "'", con))
            {
                SqlDataReader r = command.ExecuteReader();
                string name = "";
                while (r.Read())
                {
                    name = r.GetValue(0).ToString();
                }
                if (name == "Salesperson")
                    return true;
                return false;

            }

        }
        public static bool UserIsAdministrator(string UserId, SqlConnection con)
        {
            int a = Convert.ToInt32(UserId);
            using (SqlCommand command = new SqlCommand("select name from Roles r join Users u on r.RoleID=u.RoleID where UserID='" + a + "'", con))
            {
                SqlDataReader r = command.ExecuteReader();
                string name = "";
                while (r.Read())
                {
                    name = r.GetValue(0).ToString();
                }
                if (name == "Administrator")
                    return true;
                return false;

            }

        }
        public static bool UserIsManager(string UserId, SqlConnection con)
        {
            int a = Convert.ToInt32(UserId);
            using (SqlCommand command = new SqlCommand("select name from Roles r join Users u on r.RoleID=u.RoleID where UserID='" + a + "'", con))
            {
                SqlDataReader r = command.ExecuteReader();
                string name = "";
                while (r.Read())
                {
                    name = r.GetValue(0).ToString();
                }
                if (name == "Manager")
                    return true;
                return false;

            }
        }

    }
}