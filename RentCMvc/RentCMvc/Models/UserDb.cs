using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RentCMvc.Models
{
    public class UserDb:DbContext
    {
        public DbSet<User> user { get; set; }
    }
}