using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionCo.ViewModels
{
    public class CustomerListVM
    {
        public class Row
        {
            public int CustomerID { get; set; }
            public string FirstName { get; set; }
            public string Username { get; set; }
            public string PhoneNumber { get; set; }

            public string LastName { get; set; }
            public string BirthDate { get; set; }
            public string Email { get; set; }

            public int AddressID { get; set; }
            public string Address { get; set; }
            public int GenderID { get; set; }
            public string Gender { get; set; }
        }

        public List<Row> rows { get; set; }
     
    }
}
