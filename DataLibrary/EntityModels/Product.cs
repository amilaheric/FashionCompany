using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLibrary.EntityModels
{
    public class Product
    {

        public int ProductID{ get; set; }
       
        public string Name { get; set; }
       

        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; }

        public double Price { get; set; }
        public string Description { get; set; }

        public string Picture { get; set; }

    }
}
