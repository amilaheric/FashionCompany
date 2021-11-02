using DataLibrary.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionCo.ViewModels
{
    public class ProductListVM
    {


        public class Row
        {
            public int ProductID { get; set; }

            public string ProductName { get; set; }

            public double Price { get; set; }
            public string Description { get; set; }

            public string Category { get; set; }

        }
        public List<Row> rows;

    }

}
