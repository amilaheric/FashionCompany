using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionCo.ViewModels
{
    public class ShoppingCartVM
    {
        public List<Row> Rows { get; set; }
        public double Total { get; set; }
        public class Row
        {
            public int ShoppingCartId { get; set; }
            public DateTime Date { get; set; }
            public string Product { get; set; }
            public double PriceOfProduct { get; set; }
            public double Quantity { get; set; }
            public double Price { get; set; } 
        }

    }
}
