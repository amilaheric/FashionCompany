using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.EntityModels
{
    public class Role:IdentityRole<int>
    {
        public string RoleName { get; set; }
    }
}
