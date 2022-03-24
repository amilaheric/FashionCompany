using DataLibrary.EntityModels;
using FashionCo.Data;
using FashionCo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionCo.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private ApplicationDbContext _applicationDbContext;
        private readonly RoleManager<Role> roleManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext applicationDbContext,
            RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
            this.roleManager = roleManager;

        }

        [HttpGet]
        public IActionResult Register()
        {

            var model = new RegisterVM
            {
                genders = _applicationDbContext.gender
                .Select(x => new SelectListItem
                {
                    Value = x.GenderID.ToString(),
                    Text = x.Name
                }).ToList(),
                Address = _applicationDbContext.address
                .Select(x => new SelectListItem
                {
                    Value = x.AddressID.ToString(),
                    Text = x.AddressName +"("+ x.City+")"
                }).ToList()
            };


            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    AddressID = model.AddressID,
                    PhoneNumber = model.PhoneNumber,
                    GenderID = model.GenderID

                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    await userManager.AddToRoleAsync(user, "Client");
                    return RedirectToAction("Index", "Home");

                }
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);






            }

            else
            {
                model = new RegisterVM
                {
                    genders = _applicationDbContext.gender
                .Select(x => new SelectListItem
                {
                    Value = x.GenderID.ToString(),
                    Text = x.Name
                }).ToList(),
                    Address = _applicationDbContext.address
                .Select(x => new SelectListItem
                {
                    Value = x.AddressID.ToString(),
                    Text = x.AddressName + "(" + x.City + ")"
                }).ToList()
                };
            }


            return View(model);


        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


       [ HttpGet]
       public IActionResult Login()
        {
            return View();
        }

        [HttpPost]


        public async Task<IActionResult> Login(LoginVM model, string returnUrl)
        {



            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                    if (!string.IsNullOrEmpty(returnUrl))
                        return LocalRedirect(returnUrl);

                else
                    {
                        var user = await userManager.FindByNameAsync(model.Email);
                        if (await userManager.IsInRoleAsync(user, "Employee"))
                            return RedirectToAction("EmployeeHome", "Employee");
                        else if (await userManager.IsInRoleAsync(user, "Client"))
                            return RedirectToAction("Index", "Home");
                        else if (await userManager.IsInRoleAsync(user, "Administrator"))
                            return RedirectToAction("AdminHome", "Admin");
                        else return RedirectToAction("Index", "Home");
                    }
                ModelState.AddModelError("", "Sorry, try again!");

            }
            return View(model);










        }




















    }
   











}
