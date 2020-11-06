﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstone.Models
{
    public class YelpBusinesses
    {
        public YelpBusiness[] businesses { get; set; }

        public string Error { get; set; }
    }

    public class YelpBusiness
    {
        public string id { get; set; }
        public string alias { get; set; }
        public string name { get; set; }
        public string image_url { get; set; }
        public bool is_closed { get; set; }
        public string url { get; set; }
        public int review_count { get; set; }
        public Category[] categories { get; set; }
        public float rating { get; set; }
        public Coordinates coordinates { get; set; }
        public string[] transactions { get; set; }
        public string price { get; set; }
        public Location location { get; set; }
        public string phone { get; set; }
        public string display_phone { get; set; }
        public float distance { get; set; }
<<<<<<< HEAD
=======
        public string Error { get; set; }
>>>>>>> 8e102d87b8494a8cfffe9712f1647cd46a2f1df7
    }

    public class Coordinates
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
    }

    public class Location
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string[] display_address { get; set; }
    }

    public class Category
    {
        public string alias { get; set; }
        public string title { get; set; }
    }

}
