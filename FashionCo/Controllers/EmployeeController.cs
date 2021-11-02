using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionCo.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult EmployeeHome()
        {
            return View();
        }
    }
}
