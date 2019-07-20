using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Assignment_3.Models
{
    public class registration
    {
        [Key]
        public int ID { set; get; }
        public string first_name { set; get; }
        public string last_name { set; get;}
        public string email { set; get; }
        public string mobile_number { set; get; }
        public string password { set; get; }
    }
    public class product
    {
        [Key]
        public int id { set; get; }
        public string title { set; get; }
        public string picture { set; get; }
        public int no_of_units { set; get; }
        public int demand { set; get; }
        public int rating { set; get; }
    }

    public class registrationDBContext : DbContext
    {
        public DbSet<registration> registration { get; set; }
        public DbSet<product> products { get; set; }
    }
}