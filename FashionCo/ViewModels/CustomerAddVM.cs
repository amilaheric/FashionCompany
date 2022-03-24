using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionCo.ViewModels
{
    public class CustomerAddVM
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }

        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }

        public string AddressID { get; set; }
        public List<SelectListItem> Address { get; set; }
        public string GenderID { get; set; }
        public List<SelectListItem> Gender { get; set; }
    }
}

