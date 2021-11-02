using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Report
{
    public class Report1Row
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }


        public static List<Report1Row> Get()
        {
            return new List<Report1Row>();
        }


    }
}