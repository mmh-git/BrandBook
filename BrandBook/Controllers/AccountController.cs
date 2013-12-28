﻿using System;
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
                HttpContext.Response.Cookies["UserID"].Value = Membership.GetUser(model.UserName).ProviderUserKey.ToString();
                SessionVars.CurrentLoggedInUser = new UserModel()
                {
                    UserID = Membership.GetUser(model.UserName).ProviderUserKey.ToString(),
                    isLoggedIn = true
                }; 
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
            HttpContext.Response.Cookies["UserID"].Value = "";
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
            
            if (HttpContext.Request.Cookies[".ASPXAUTH"] == null)
            {
                is_LoggedIn.Add("loggedIn", false);
                HttpContext.Response.Cookies["UserID"].Value = "";
            }
            else
            {
                string autheticCookieVal = Request.Cookies[FormsAuthentication.FormsCookieName].Value;
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(autheticCookieVal);
                if (!ticket.Expired)
                {
                    
                    is_LoggedIn.Add("loggedIn", true);
                }
                else{
                    is_LoggedIn.Add("loggedIn", false);
                    HttpContext.Response.Cookies["UserID"].Value = "";
                }
                
            }

            

            //if (this.User.Identity.IsAuthenticated)
            //{
            //    SessionVars.CurrentLoggedInUser.isLoggedIn = true;
            //    is_LoggedIn.Add("loggedIn", true);

            //}
            //else
            //{
            //    SessionVars.CurrentLoggedInUser = null;
            //        is_LoggedIn.Add("loggedIn", false);
 
            //}
            return Json(is_LoggedIn, JsonRequestBehavior.AllowGet);
        }



        
    }
}