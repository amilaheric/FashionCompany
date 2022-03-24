using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.EntityModels
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }

    }
}
