using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandBookModel
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
