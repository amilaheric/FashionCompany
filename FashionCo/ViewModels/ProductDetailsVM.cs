using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionCo.ViewModels
{
    public class ProductDetailsVM
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }

        public double Price { get; set; }
        public string Description { get; set; }

        public int CategoryID { get; set; }
        public string Category { get; set; }
        public IFormFile productPictureNew { set; get; }

        public string PictureUrl { get; set; }
    }
}
