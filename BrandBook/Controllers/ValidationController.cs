using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BrandBook.Controllers
{
    public class ValidationController : Controller
    {
        public JsonResult IsUserAvailable(string UserName)
        {
            if (Membership.FindUsersByName(UserName).Count != 0)
            {
                string msg = string.Format("{0} is not available", UserName);
                return Json(msg,JsonRequestBehavior.AllowGet);
            }
            else
            {
                
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
