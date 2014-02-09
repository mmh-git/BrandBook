using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using BrandBook.Filters;
using BrandBook.Models;
using BrandBookModel;
using BrandBook.Helper;
using BrandBookBiz;
namespace BrandBook.Controllers
{
   
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        
        //
        // POST: /Account/Login

        [HttpPost]
        
        public string Login(LoginModel model)
        {
            string isLoggedIn = "failed";
            if (ModelState.IsValid && Membership.ValidateUser(model.UserName,model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName,model.RememberMe);
                CookieManager.AddCookie("UserID",Membership.GetUser(model.UserName).ProviderUserKey.ToString());
                UserModel userModel = new UserModel();
                userModel.UserID = Membership.GetUser(model.UserName).ProviderUserKey.ToString();
                userModel.isLoggedIn = true;
                BrandBookFacadeBiz facade = new BrandBookFacadeBiz();
                userModel= facade.GetUserDetails(userModel);
                CookieManager.AddCookie("UserDetaisID",userModel.UserDetailsID.ToString());
                CookieManager.AddCookie("UserName", userModel.UserName.ToString());
                CookieManager.AddCookie("FirstName", userModel.FirstName.ToString());
                isLoggedIn = "done";
            }
            return isLoggedIn;
            // If we got this far, something failed, redisplay form
            //ModelState.AddModelError("", "The user name or password provided is incorrect.");
            //return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public string LogOff()
        {
            FormsAuthentication.SignOut();
            CookieManager.RemoveAllCookies();
            return "success";
        }

        //
        // GET: /Account/Register

        

        //
        // POST: /Account/Register

        [HttpPost]
        //[AllowAnonymous]
       // [ValidateAntiForgeryToken]
        public string SignUp(RegisterModel model)
        {
            
            MembershipCreateStatus status=MembershipCreateStatus.ProviderError;
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    
                    //WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    //WebSecurity.Login(model.UserName, model.Password);
                    Membership.CreateUser(model.UserName,model.Password,model.Email,"test","test",true,out status);
                    MembershipUser membershipUser=Membership.GetUser(model.UserName);
                    UserModel userModel = new UserModel() { 
                    UserID=membershipUser.ProviderUserKey.ToString(),
                    UserName=model.UserName,
                    FirstName=model.FirstName,
                    LastName=model.LastName,
                    DateOfBirth=model.DateOfBirth,
                    Desination=model.Designation,
                    Gender=model.Gender
                    };
                    userModel = new UserController().SaveUserDetails(userModel);
                    if (userModel.Message == "error")
                    {
                        Membership.DeleteUser(model.UserName);
                        status = MembershipCreateStatus.ProviderError;
                    }

                    else if (userModel.Message == "done")
                    {
                       string isLoggedIn =Login(new LoginModel { UserName = model.UserName, Password = model.Password });
                       if (isLoggedIn == "done")
                       {
                           status = MembershipCreateStatus.Success;
                       }
                    }
                }
                catch (MembershipCreateUserException e)
                {
                   
                }
            }
            return status.ToString();
            // If we got this far, something failed, redisplay form
            
        }

        //[AllowAnonymous]

        public ActionResult IsLogedIn()
        {
            Dictionary<string, bool> is_LoggedIn = new Dictionary<string,bool>();
            bool isAuthentic = false;
            if (HttpContext.Request.Cookies[".ASPXAUTH"] == null)
            {
                isAuthentic= false;
                HttpContext.Response.Cookies["UserID"].Value = "";
            }
            else
            {
                string autheticCookieVal = Request.Cookies[FormsAuthentication.FormsCookieName].Value;
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(autheticCookieVal);
                if (!ticket.Expired)
                {
                    
                    isAuthentic= true;
                }
                else{
                    isAuthentic= false;
                    HttpContext.Response.Cookies["UserID"].Value = "";
                }

            }


            //if (this.User.Identity.IsAuthenticated)
            //{

            //    isAuthentic=true;

            //}
            //else
            //{

            //    isAuthentic = false;

            //}
            is_LoggedIn.Add("loggedIn", isAuthentic);
            return Json(is_LoggedIn, JsonRequestBehavior.AllowGet);
        }



        
    }
}
