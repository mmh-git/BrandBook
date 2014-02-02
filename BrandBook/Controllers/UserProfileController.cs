using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrandBookModel;
using BrandBookBiz;
namespace BrandBook.Controllers
{
    public class UserProfileController : Controller
    {
        //
        // GET: /UserProfile/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getUserProfile() 
        {
            BrandBookFacadeBiz facade = new BrandBookFacadeBiz();
            UserModel userModel=Helper.CookieManager.ReloadSessionFromCookie();
            UserProfile profile=facade.GetUserProfile(userModel);
            return View("UserProfilePartial", profile);
        }
    }
}
