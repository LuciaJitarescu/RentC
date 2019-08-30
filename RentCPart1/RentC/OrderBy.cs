using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace RentC
{
    class OrderBy
    {
        public static DataTable OrderByModel(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "Model ASC";
            dt = dv.ToTable();
            return dt;
        }
        public static DataTable OrderByModelDesc(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "Model DESC";
            dt = dv.ToTable();
            return dt;
        }
        public static DataTable OrderByPlate(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "Plate ASC";
            dt = dv.ToTable();
            return dt;

        }
       public  static DataTable OrderByNumberOfRents(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "NumberOfRents ASC";
            dt = dv.ToTable();
            return dt;

        }
       public  static DataTable OrderByNumberOfRentsDesc(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "NumberOfRents DESC";
            dt = dv.ToTable();
            return dt;

        }
        public static DataTable OrderByPlateDesc(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "Plate DESC";
            dt = dv.ToTable();
            return dt;

        }
        public static DataTable OrderByCarID(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "CarID ASC";
            dt = dv.ToTable();
            return dt;
        }

        public static DataTable OrderByCarIDDesc(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "CarID DESC";
            dt = dv.ToTable();
            return dt;
        }
        public static DataTable OrderByPricePerDay(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "PricePerDay ASC";
            dt = dv.ToTable();
            return dt;
        }
        public static DataTable OrderByName(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "Name ASC";
            dt = dv.ToTable();
            return dt;
        }
        public static DataTable OrderByNameDesc(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "Name DESC";
            dt = dv.ToTable();
            return dt;
        }

        public static DataTable OrderByBirthDate(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "BirthDate ASC";
            dt = dv.ToTable();
            return dt;
        }
        public static DataTable OrderByBirthDateDesc(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "BirthDate DESC";
            dt = dv.ToTable();
            return dt;
        }

        public static DataTable OrderByLocation(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "Location ASC";
            dt = dv.ToTable();
            return dt;

        }
        public static DataTable OrderByLocationDesc(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "Location DESC";
            dt = dv.ToTable();
            return dt;

        }
        public static DataTable OrderByEndDate(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "EndDate ASC";
            dt = dv.ToTable();
            return dt;
        }
        public static DataTable OrderByStartDateDesc(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "StartDate DESC";
            dt = dv.ToTable();
            return dt;
        }
        public static DataTable OrderByEndDateDesc(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "EndDate DESC";
            dt = dv.ToTable();
            return dt;
        }
        public static DataTable OrderByManufacturer(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "Manufacturer ASC";
            dt = dv.ToTable();
            return dt;
        }
        public static DataTable OrderByManufacturerDesc(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "Manufacturer DESC";
            dt = dv.ToTable();
            return dt;
        }
        public static DataTable OrderByCustomerId(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "CostumerID ASC";
            dt = dv.ToTable();
            return dt;
        }
        public static DataTable OrderByCustomerIdDesc(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "CostumerID DESC";
            dt = dv.ToTable();
            return dt;

        }

        public static DataTable OrderByPricePerDayDesc(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "PricePerDay DESC";
            dt = dv.ToTable();
            return dt;
        }
        public static DataTable OrderByStartDate(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "StartDate ASC";
            dt = dv.ToTable();
            return dt;
        }
    }
}
