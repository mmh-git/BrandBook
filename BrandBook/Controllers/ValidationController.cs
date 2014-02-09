using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BrandBookBiz;
namespace BrandBook.Controllers
{
    public class ValidationController : Controller
    {
        
        
        public JsonResult IsUserAvailable(string UserName)
        {
            BrandBookFacadeBiz facade = new BrandBookFacadeBiz();
            if (Membership.FindUsersByName(UserName).Count != 0)
            {
                string msg = string.Format("{0} is not available", UserName);
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else if (facade.ValidateUserName(UserName) == 0)
            {
                string msg = string.Format("{0} is not valid. Get Bpl Initial from authority", UserName);
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else 
            {

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            
        }
        public JsonResult IsEmailAvailable(string Email)
        {
            BrandBookFacadeBiz facade = new BrandBookFacadeBiz();
            if (Membership.FindUsersByEmail(Email).Count != 0)
            {
                string msg = string.Format("Email address is already taken");
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            if (facade.ValidateEmail(Email) == 0)
            {
                string msg = string.Format("{0} is not valid. Get Email address from authority", Email);
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else 
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            
        }

    }
}
