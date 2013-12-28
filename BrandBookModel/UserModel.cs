using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrandBookModel
{
    public class UserModel
    {
        public string UserID { get; set; }
        public int UserDetailsID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Desination { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string ProfilePicID { get; set; }
        public string ProfilePicUrl { get; set; }
        public string Message { get; set; }
        public bool isLoggedIn { get; set; }
    }
}
