using RentCMvc.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data.Entity;
using System.Data;

namespace RentCMvc.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(User u)
        {
            using (var context = new UserEntities())
            {
                bool valid = context.Users.Any(x => x.UserID == u.UserID && x.Password == u.Password);
                if (valid)
                { 
                    roleS = UsersAndPermissions.UserIsSalesperson(u.UserID.ToString(), con);
                    roleM = UsersAndPermissions.UserIsManager(u.UserID.ToString(), con);
                    roleA = UsersAndPermissions.UserIsAdministrator(u.UserID.ToString(), con);
                    return RedirectToAction("RegisterNewCarRent");
                }
                else
                    return RedirectToAction("SignIn");
            }
           
        }

        static bool roleS = false;
        static bool roleA = false;
        static bool roleM = false;
        static SqlConnection con = DatabaseConnection.Conecting_to_database();

        public ActionResult RegisterNewCarRent()
        {
            //check if the user is logged in
            if (roleA || roleM || roleS)
            {
                return View();
            }
            //if not, redirect to page SignIn
            else
                return RedirectToAction("SignIn");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterNewCarRent(Models.NewRent model)
        {
            SqlConnection con = Models.DatabaseConnection.Conecting_to_database();
            if (ModelState.IsValid)
            {
                string carId = Models.RegisterNewRent.CarID_from_plate(model.Plate, con);
                if (carId != "" && Models.RegisterNewRent.DataValidation(model.Start) && RegisterNewRent.DataValidation(model.end))
                    if (RegisterNewRent.ClientExist(con, int.Parse(model.ClientID)) && RegisterNewRent.CarAvailable(con, carId, DateTime.Parse(model.Start), DateTime.Parse(model.end)) && RegisterNewRent.CheckDate(DateTime.Parse(model.Start), DateTime.Parse(model.end)))

                    {
                        RegisterNewRent.Register_car_rent(carId, model.ClientID, "1", model.Start, model.end, model.City, RegisterNewRent.Randome_cupon(con), con);
                        return RedirectToAction("SignIn");
                    }
            }
            return View();
        }



        public ActionResult RegisterNewCustomer()
        {
            if (roleA == true || roleM == true)
            {
                return View();
            }
            return RedirectToAction("SignIn");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterNewCustomer(Models.Customer model)
        {
            SqlConnection con = DatabaseConnection.Conecting_to_database();
            if (ModelState.IsValid)
            {
                if (model.CostumerID.ToString() != "")
                    if (!RegisterCustomer.ClientExist(con, int.Parse(model.CostumerID.ToString())) && RegisterCustomer.ZipValidation(model.Location))
                    {
                        RegisterCustomer.Register_New_Customer(model.Location, model.Name, model.BirthDate.ToString("yyyy-MM-dd"), model.CostumerID.ToString(), con);
                        return RedirectToAction("SignIn");
                    }
                
            }
            return View();
        }
        
        public ActionResult UpdateCustomer()
        {
            if (roleA == true || roleM == true)
            {
                return View();
            }
            return RedirectToAction("SignIn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCustomer(Models.Customer model)
        {

            SqlConnection con = DatabaseConnection.Conecting_to_database();
            if (ModelState.IsValid)
            {
                if (model.CostumerID.ToString() != "")
                    if (RegisterCustomer.ClientExist(con, int.Parse(model.CostumerID.ToString()))&& RegisterCustomer.ZipValidation(model.Location))
                    {
                        Update_Customer.Update_Client(model.Location, model.Name, model.BirthDate.ToString("yyyy-MM-dd"), model.CostumerID.ToString(), con);
                        return RedirectToAction("SignIn");
                    }
            }
            return View();
        }

        public ActionResult UpdateCarRent()
        {
            if (roleS || roleA || roleM)
            {
                return View();
            }
            else
                return RedirectToAction("SignIn");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCarRent(Models.NewRent model)
        {
            SqlConnection con = DatabaseConnection.Conecting_to_database();
            if (ModelState.IsValid)
            {
                string carId = RegisterNewRent.CarID_from_plate(model.Plate, con);
                DateTime start = DateTime.Parse(model.Start);
                DateTime end = DateTime.Parse(model.end);
                if (carId != "" && Models.RegisterNewRent.DataValidation(model.Start) && RegisterNewRent.DataValidation(model.end))
                    if (RegisterNewRent.CarAvailable(con, carId, start, end) && RegisterNewRent.ClientExist(con, int.Parse(model.ClientID)) && RegisterNewRent.CheckDate(start, end))
                    {
                        Update_Rent.update_rent(con, model.ClientID, carId, start.ToString(), end.ToString(), model.City);
                        return RedirectToAction("SignIn");
                    }
            }
            return View();
        }



        public ActionResult List_Customers()
        {
            if (roleA == true || roleM == true)
            {
               // ViewBag.Message = "Customers list";
                var data = DatabaseConnection.LoadData<Customer>(" select * from Customers");
                List<Customer> list = new List<Customer>();
                foreach (var i in data)
                {
                    list.Add(new Customer
                    {
                        CostumerID = i.CostumerID,
                        Name = i.Name,
                        BirthDate = i.BirthDate,
                        Location = i.Location
                    });
                }

                return View(list);
            }
            return RedirectToAction("SignIn");

        }

        public ActionResult List_Rents()
        {
            if (roleM || roleS || roleA)
            {
                //ViewBag.Message = "Rents list";
                var data = DatabaseConnection.LoadData<Rents>(" select * from Reservations");
                List<Rents> list = new List<Rents>();
                foreach (var i in data)
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
                }

                return View(list);
            }
            else
                return RedirectToAction("SignIn");


        }

        public ActionResult List_Cars()
        {
            if (roleA == true || roleM == true)
            {
                ViewBag.Message = "Car list";
                var data = DatabaseConnection.LoadData<Car>(" select * from Cars");
                List<Car> list = new List<Car>();
                foreach (var i in data)
                {
                    list.Add(new Car
                    {
                        CarID = i.CarID,
                        Plate = i.Plate,
                        Manufacturer = i.Manufacturer,
                        Model = i.Model,
                        PricePerDay = i.PricePerDay

                    });

                }
                return View(list);
            }
            return RedirectToAction("SignIn");


        }


    }
}