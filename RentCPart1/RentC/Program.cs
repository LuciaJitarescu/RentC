using System;
using System.Data.SqlClient;


namespace RentC
{
    class Program
    {
        static void Main(string[] args)
        {
            UpdateReservatios();
            Console.WriteLine("Welcome to RentC, your brand new solution ");
            Console.WriteLine("to manage and control your company's data ");
            Console.WriteLine("without missing anything.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue or ESC to quit");
            // Console.ReadKey(true);
            ConsoleKeyInfo cki;
            while (true)
            {
                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                if (cki.Key == ConsoleKey.Enter)
                {
                    MenuScreen();
                }
            }
        }
        public static void MenuScreen()
        {
            Console.Clear();
            Console.WriteLine("Menu Screen");
            Console.WriteLine();
            Console.WriteLine("1. Register new Car Rent");
            Console.WriteLine("2. Update Car Rent");
            Console.WriteLine("3. List Rents");
            Console.WriteLine("4. List Available Cars");
            Console.WriteLine("5. Register new Customer");
            Console.WriteLine("6. Update Customer");
            Console.WriteLine("7. List Customers");
            Console.WriteLine("8. Gold/Silver Customers");
            Console.WriteLine("9. Most Recent Rented Cars");
            Console.WriteLine("10. Most Rented Cars");
            Console.WriteLine("11. Less rented Cars");
            Console.WriteLine("12. Quit");
            Console.WriteLine();
            Console.WriteLine("Please, type the number associated with the option you want and the press ENTER");
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MenuScreen());
            int number;
            //ConsoleKeyInfo cki= Console.ReadKey()
            try
            {
                number = int.Parse(Console.ReadLine());
                
                if (number < 1 || number > 12)
                {
                    Console.WriteLine("This is not a valid option");
                }
                switch (number)
                {
                    case 1:
                        Register_New_Rent.RegisterNewCarRent();
                        break;
                    case 2:
                        Update_Rent.UpdateCarRent();
                        break;
                    case 3:
                        List_Rents.ListRents();
                        break;
                    case 4:
                        List_Available_Cars.ListAvailableCars();
                        break;
                    case 5:
                        Register_New_Customer.RegisterNewCustomer();
                        break;
                    case 6:
                        Update_Customer.UpdateCustomer();
                        break;
                    case 7:
                        List_Customers.ListCustomers();
                        break;
                    case 8:
                        Golden_Silver_Customers.GoldSilverCostumers();
                        break;
                    case 9:
                        Most_Recent_Rented_Cars.MostRecentRentedCars();
                        break;
                    case 10:
                        Most_Less_Rented_Cars.MostRentedCars();
                        break;
                    case 11:
                        Most_Less_Rented_Cars.LessRentedCars();
                        break;
                    case 12:
                        Environment.Exit(0);
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("This is not a valid option");
            }
        }
        static void UpdateReservatios()
        {
            string conString = "Data Source=DESKTOP-MJA5A7T;Initial Catalog=Car Rent;Integrated Security=True";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            //change reserveStatsId to Closed
            SqlCommand update = new SqlCommand("update Reservations set ReservStatsID=3 where EndDate<GETDATE()", con);
            update.ExecuteReader();
        }
    }
}

