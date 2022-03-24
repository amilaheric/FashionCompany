using DataLibrary.EntityModels;
using FashionCo.Data;
using FashionCo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionCo.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {

        ApplicationDbContext _db;

        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet("customerlist")]
        public IActionResult CustomerList()
        {

            List<CustomerListVM.Row> customers = _db.user
                .Select(x => new CustomerListVM.Row
            {
                Address = x.Address.AddressName,
                AddressID = x.AddressID,
                BirthDate = x.BirthDate.ToString("yyyy/MM/dd"),
                CustomerID=x.Id,
                Email=x.Email,
                FirstName=x.FirstName,
                Gender=x.Gender.Name,
                GenderID=x.GenderID,
                LastName=x.LastName,
                PhoneNumber=x.PhoneNumber,
                Username=x.UserName

              }).ToList();
            CustomerListVM c = new CustomerListVM();
            c.rows = customers;
            



            return Ok(customers);
        }


       [HttpGet("delete")]
        public IActionResult Delete(int CustomerID)
        {
            User customers = _db.user.Find(CustomerID);
            _db.Remove(customers);
            _db.SaveChanges();

            return Ok();
        }


        [HttpGet("details")]
        public IActionResult Details(int CustomerID)
        {
            var c = _db.user.Where(x => x.Id == CustomerID)
                .Select(i => new CustomerDetailsVM
                {
                    CustomerID = i.Id,
                    Email = i.Email,
                    SelectedAddress = i.Address.AddressName,
                    SelectedGender = i.Gender.Name,
                    BirthDate =i.BirthDate.ToString("yyyy/MM/dd"),
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    PhoneNumber = i.PhoneNumber,
                    Username = i.UserName
                }).FirstOrDefault();
             


            return Ok(c);
        }

       

        [HttpGet("AddCustomer")]
        public IActionResult AddCustomer()
        {

            CustomerAddVM customer = new CustomerAddVM
            {
                Address = _db.address.Select(x => new SelectListItem
                {
                    Text = x.AddressName,
                    Value = x.AddressID.ToString(),

                }).ToList(),

                Gender = _db.gender.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.GenderID.ToString()

                }).ToList()
            };

            return Ok(customer);
        }



          [HttpPost("AddSave")]
        public IActionResult AddSave([FromBody] CustomerAddVM vm)
                {
            var addr = Int32.Parse(vm.AddressID);
            var gen = Int32.Parse(vm.GenderID);



                User novi = new User
            {
                BirthDate =Convert.ToDateTime(vm.BirthDate),
                Email = vm.Email,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Id = vm.CustomerID,
                PhoneNumber = vm.PhoneNumber,
                UserName = vm.Username,
                AddressID=addr,
                GenderID=gen

            };
            _db.Add(novi);
            _db.SaveChanges();


            return Ok();
        }


        [HttpGet("edit")]
        public IActionResult Edit(int CustomerID)
        {
            var u = _db.user.Where(x => x.Id == CustomerID)
                 .Include(x => x.Address)
                .Include(x => x.Gender)
                .FirstOrDefault();
          

            CustomerEditVM model = new CustomerEditVM
            {
                CustomerID = u.Id,
                LastName = u.LastName,
                FirstName = u.FirstName,
                Username = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                BirthDate = u.BirthDate.ToString("yyyy/MM/dd"),
                AddressID =u.AddressID.ToString(),
                GenderID = u.GenderID.ToString(),




            };


            return Ok(model);
        }

        [HttpPost("SaveEdit")]
        public IActionResult SaveEdit([FromBody] CustomerEditVM vm)
        {


            User u = _db.user.Where(x => x.Id == vm.CustomerID)
                .First();

            u.LastName = vm.LastName;
            u.FirstName = vm.FirstName;
            u.GenderID = Int32.Parse(vm.GenderID);
            u.UserName = vm.Username;
            u.BirthDate = Convert.ToDateTime(vm.BirthDate);
            u.Email = vm.Email;
            u.PhoneNumber = vm.PhoneNumber;
            u.AddressID = Int32.Parse(vm.AddressID);

            _db.SaveChanges();


            return Ok();
        }
        


    }
}
