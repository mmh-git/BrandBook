using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrandBookModel;
using BrandBookBiz;
using BrandBook.Helper;
using System.Web.Security;
namespace BrandBook.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        BrandBookFacadeBiz _biz;
        public ActionResult Index()
        {
            BrandBookFacadeBiz _biz = new BrandBookFacadeBiz();

            UserModel userModel = new UserModel() 
            {
                UserID=Membership.GetUser(this.User.Identity.Name).ProviderUserKey.ToString()
            };

            userModel = _biz.GetUserDetails(userModel);
            return PartialView("UserPartial", userModel);
        }

        public UserModel GetUserDetails(UserModel userModel)
        {
            _biz = new BrandBookFacadeBiz();
            try
            {
                userModel = _biz.GetUserDetails(userModel);
                userModel.Message = "done";
            }
            catch 
            {
                userModel.Message = "error";
            }
            return userModel;
 
        }

        public UserModel SaveUserDetails(UserModel userModel)
        {
            
            try
            {
                _biz = new BrandBookFacadeBiz();
                userModel = _biz.SaveUserDetails(userModel);
                userModel.Message = "done";
            }
            catch
            {
                userModel.Message = "error";
            }
            return userModel;
        }

    }
}
