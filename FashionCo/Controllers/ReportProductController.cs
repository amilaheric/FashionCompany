using AspNetCore.Reporting;
using DataLibrary.EntityModels;
using FashionCo.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionCo.Controllers
{
    public class ReportProductController : Controller
    {

        private ApplicationDbContext _db;
        private UserManager<User> userManager;
        public ReportProductController(ApplicationDbContext db, UserManager<User> _userManager)
        {
            _db = db;
        }
        public IActionResult Index(int userid)
        {


            AspNetCore.Reporting.LocalReport _localReport = new AspNetCore.Reporting.LocalReport("Reports/Report1.rdlc");
            List<Report1Row> data = getProducts(_db);
            _localReport.AddDataSource("DataSet1", data);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ReportSastavio", User.Identity.Name);

            ReportResult result = _localReport.Execute(RenderType.Pdf, parameters: parameters);
            return File(result.MainStream, "application/pdf");





            return View();
        }



        public static List<Report1Row>getProducts(ApplicationDbContext db)
        {
            List<Report1Row> data = db.product.Select(x => new Report1Row
            {

                Name = x.Name,
                Category = x.Category.CategoryName,
                Price = x.Price,
                Description = x.Description

            }).ToList();


            return data;
        }


    }
}
