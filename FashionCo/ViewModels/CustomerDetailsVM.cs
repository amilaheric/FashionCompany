using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionCo.ViewModels
{
    public class CustomerDetailsVM
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string SelectedAddress { get; set; }
        public string SelectedGender { get; set; }
    }
}
