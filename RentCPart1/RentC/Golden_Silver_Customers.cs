using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace RentC
{
    class Golden_Silver_Customers
    {
        public static void GoldSilverCostumers()
        {
            Console.Clear();
            string conString = "Data Source=DESKTOP-MJA5A7T;Initial Catalog=Car Rent;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter("select Name, c.CostumerID from Customers c join Reservations r on c.CostumerID=r.CostumerID where StartDate between GETDATE()-30 and GETDATE()", con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            DataTable newTable = new DataTable();
            newTable.Clear();
            newTable.Columns.Add("CostumerID");
            newTable.Columns.Add("Name");
            newTable.Columns.Add("Gold/Silver");
            foreach (DataRow row in dt.Rows)
            {
                int count = CountRentsForCustomer(row["CostumerID"].ToString(), dt);
                if (count >= 4)
                {
                    DataRow gold = newTable.NewRow();
                    gold["CostumerID"] = row["CostumerID"].ToString();
                    gold["Name"] = row["Name"].ToString();
                    gold["Gold/Silver"] = "Gold";
                    newTable.Rows.Add(gold);
                }
                else if (count >= 2)
                {
                    DataRow silver = newTable.NewRow();
                    silver["CostumerID"] = row["CostumerID"].ToString();
                    silver["Name"] = row["Name"].ToString();
                    silver["Gold/Silver"] = "Silver";
                    newTable.Rows.Add(silver);
                }
            }

            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            foreach (DataRow drow in newTable.Rows)
            {
                if (hTable.Contains(drow["CostumerID"]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow["CostumerID"], string.Empty);
            }

            foreach (DataRow dRow in duplicateList)
                newTable.Rows.Remove(dRow);

            int width = Console.WindowWidth;
            width = width / 3;
            Write.WriteProgress("CustomerID", 1, 1);
            Write.WriteProgress("Name", width, 1);
            Write.WriteProgress("Gold/Silver", 2 * width, 1);
            int r = 3;

            foreach (DataRow row in newTable.Rows)
            {
                Write.WriteProgress(row["CostumerID"].ToString(), 1, r);
                Write.WriteProgress(row["Name"].ToString(), width, r);
                Write.WriteProgress(row["Gold/Silver"].ToString(), 2 * width, r);
                r++;
            }
        }
        public static int CountRentsForCustomer(string customerId, DataTable dt)
        {
            int count = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (row["CostumerID"].ToString() == customerId)
                    count++;
            }
            return count;
        }

    }
}
