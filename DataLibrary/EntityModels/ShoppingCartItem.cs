using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.EntityModels
{
    public class ShoppingCartItem
    {
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
