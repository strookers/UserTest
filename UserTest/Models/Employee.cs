using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserTest.Models
{
    public class Employee : Person
    {
        public string Address { get; set; }
        public int ZipCode { get; set; }

    }
}