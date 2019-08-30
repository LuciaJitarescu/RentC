using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentCMvc.Models;
using System.Data.SqlClient;
using System.Data;

namespace RentCMvc.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult MostRecentRentedCars()
        {
            
            var data = DatabaseConnection.LoadData<Rents>("select * from Reservations");
            data = data.OrderByDescending(x=>x.EndDate).ToList();
            List<Rents> list = new List<Rents>();
            int count = 0;
            foreach (var i in data)
            {
                if (i.EndDate < DateTime.Now&&count<=20)
                {
                    list.Add(new Rents
                    {
                        CostumerID = i.CostumerID,
                        CarID = i.CarID,
                        ReservStatusID = i.ReservStatusID,
                        StartDate = i.StartDate,
                        EndDate = i.EndDate,
                        CouponCode = i.CouponCode,
                        Location = i.Location
                    });
                    count++;
                }
                
            }

            return View(list);
        }
        public ActionResult NumberOfRents()
        {
            return View();
        }
        string month = "9";
        string year = "2019";
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NumberOfRents(Models.MonthAndYear model)
        {
            //month = model.month;
            //year = model.year;
                                                                                                                                        
            return RedirectToAction("CountRents");
        }
        public ActionResult CountRents()
        {
            SqlConnection con = DatabaseConnection.Conecting_to_database();
            var data = DatabaseConnection.LoadData<Rents>("select CarID, StartDate from Reservations");
            List<CarsWithNumberOfRents> list = new List<CarsWithNumberOfRents>();
            foreach (var i in data)
            {
                if (i.StartDate.Month.ToString() == month && i.StartDate.Year.ToString() == year)
                {
                    list.Add(new CarsWithNumberOfRents
                    {
                        CarID = i.CarID,
                        NrOfRents = CountsOfRentsForEveryCar.NOfRents(i.CarID)
                    });
                }

            }
            list = list.Distinct().ToList();
            return View(list.Distinct());
        }
        public ActionResult GoldCustomer()
        {
            SqlConnection con = DatabaseConnection.Conecting_to_database();
            var data = DatabaseConnection.LoadData<Rents>("select CostumerID from Reservations");
            List<CustomerRents> list = new List<CustomerRents>();
            foreach (var i in data)
            {
                int count = CountRentsForCustomer.CountRents(i.CostumerID);
                if (count>=4)
                {
                    list.Add(new CustomerRents
                    {
                        CustomerID = i.CarID,
                        NrOfRents = count
                    }) ;
                }

            }
            list = list.Distinct().ToList();
            return View(list.Distinct());
        }
        public ActionResult SilverCustomer()
        {
            SqlConnection con = DatabaseConnection.Conecting_to_database();
            var data = DatabaseConnection.LoadData<Rents>("select CostumerID from Reservations");
            List<CustomerRents> list = new List<CustomerRents>();
            foreach (var i in data)
            {
                int count = CountRentsForCustomer.CountRents(i.CostumerID);
                if ( count >= 2&&count<4)
                {
                    list.Add(new CustomerRents
                    {
                        CustomerID = i.CostumerID,
                        NrOfRents = count
                    });
                }

            }
            list = list.Distinct().ToList();
            return View(list.Distinct());
        }

    }
}