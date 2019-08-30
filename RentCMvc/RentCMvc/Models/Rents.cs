using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentCMvc.Models
{
    public class Rents
    {
        public int CarID { get; set; }
        public int CostumerID { get; set; }
        public int ReservStatusID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string CouponCode { get; set; }
        
    }
}