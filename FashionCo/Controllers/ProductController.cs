using DataLibrary.EntityModels;
using FashionCo.Data;
using FashionCo.SignalR;
using FashionCo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FashionCo.Controllers
{
    [Authorize(Roles = "Employee")]
    public class ProductController : Controller
    {
        ApplicationDbContext _db;
        IHubContext<MyHub> _hubContext;
        private readonly UserManager<User> userManager;
        public ProductController(ApplicationDbContext db, IHubContext<MyHub> hubContext, UserManager<User> userManager)
        {
            _db = db;
            _hubContext = hubContext;
            this.userManager = userManager;
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


        public IActionResult AddProduct(int ProductID)
        {

            //var categories = new ProductAddVM
            //{

            //    Category = _db.category.Select(a => new SelectListItem
            //    {
            //        Text = a.CategoryName,
            //        Value = a.CategoryID.ToString()
            //    })
            //      .ToList()
            //};

            List<SelectListItem> categ = _db.category
              
               .Select(a => new SelectListItem
               {
                   Text = a.CategoryName,
                   Value = a.CategoryID.ToString()
               })
               .ToList();

            ProductAddVM p;
                if (ProductID == 0)
                p = new ProductAddVM() {  };
                else

                p=_db.product
                .Where(w => w.ProductID == ProductID)
                .Select(a => new ProductAddVM
                {
                    ProductID = a.ProductID,
                    ProductName = a.Name,
                    Price = a.Price,
                    CategoryID = a.CategoryID,
                    Description = a.Description,
                    PictureUrl = a.Picture
                    //Category=categ

                }).Single();
            p.Category = categ;






            return View("AddProduct",p);
        }


        public IActionResult Save(ProductAddVM vm)
        {
          

            Product product;

            if (vm.ProductID == 0)     
            {
                product = new Product();
                _db.Add(product);
                
            }
            else
            {                              
                product = _db.product.Find(vm.ProductID);
               
            }

            product.Name = vm.ProductName;
            product.Price = vm.Price;
            product.CategoryID = vm.CategoryID;
            product.Description = vm.Description;




            if (vm.productPictureNew != null)
            {
                string ekstenzija = Path.GetExtension(vm.productPictureNew.FileName);
                string contentType = vm.productPictureNew.ContentType;

                var filename = $"{Guid.NewGuid()}{ekstenzija}";
                string folder = "uploads/";
                bool exists = System.IO.Directory.Exists(folder);
                if (!exists)
                    System.IO.Directory.CreateDirectory(folder);

                vm.productPictureNew.CopyTo(new FileStream(folder + filename, FileMode.Create));
                product.Picture = filename;
            }



            _db.SaveChanges();


            string poruka =" proizvod je evidentiran ";

            _hubContext.Clients.Group(User.Identity.Name).SendAsync("prijemPoruke", User.Identity.Name, poruka);
            //_hubContext.Clients.All.SendAsync("prijemPoruke", User.Identity.Name, poruka);


            return Redirect("/Product/ProductList");




        }


        public IActionResult DeleteProduct(int productID)
        {
            Product p = _db.product.Find(productID);

            _db.Remove(p);
            _db.SaveChanges();
            return Redirect("/Product/ProductList");
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


    }
}
