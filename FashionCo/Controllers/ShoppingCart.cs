using FashionCo.Data;
using FashionCo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionCo.Controllers
{
    public class ShoppingCart : Controller
    {
        ApplicationDbContext _db;
        public ShoppingCart(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult ProductList()
        {
            var model = new ProductListVM
            {
                rows = _db.product.Select(i => new ProductListVM.Row
                {

                    ProductID = i.ProductID,
                    ProductName = i.Name,
                    Price = i.Price,
                    Category = i.Category.CategoryName,
                    Description = i.Description

                }).ToList()


            };


            return View(model);
        }

        public IActionResult ProductDetails(int productID)
        {

            var p = _db.product
              .Where(w => w.ProductID == productID)
              .Select(a => new ProductDetailsVM
              {
                  ProductID = a.ProductID,
                  ProductName = a.Name,
                  Price = a.Price,
                  CategoryID = a.CategoryID,
                  Description = a.Description,
                  PictureUrl = a.Picture,
                  Category = a.Category.CategoryName


              }).Single();

            return View(p);
        }

        public IActionResult ViewDetailCart(int id)
        {
            
            var cart = _db.ShoppingCart
                .Where(i => i.Id == id)
                .SingleOrDefault();

            var prducts = _db.ShoppingCartItem
                .Include(i => i.Product)
                .Where(i => i.ShoppingCartId == id)
                .ToList();

           

            return View();

        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
