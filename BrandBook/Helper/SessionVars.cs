using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrandBookModel;
namespace BrandBook.Helper
{
    public static class SessionVars
    {
        public static UserModel CurrentLoggedInUser
        {
            get 
            {
                return HttpContext.Current.Session["CurrentLoggedInUser"] as UserModel;
            }
            set 
            {
                HttpContext.Current.Session["CurrentLoggedInUser"] = value;
            }
        }
    }
}