using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrandBookModel;
using BrandBookBiz;
using System.Threading;
using System.Web.Script.Serialization;
using BrandBook.Helper;
namespace BrandBook.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class UserProfileController : Controller
    {
        //
        // GET: /UserProfile/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getUsers()
        {
            BrandBookFacadeBiz facade = new BrandBookFacadeBiz();
            return Json(facade.GetUserProfile(new UserModel() { UserDetailsID=0}).user, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getUserProfile(int? userDetailsId) 
        {
            BrandBookFacadeBiz facade = new BrandBookFacadeBiz();
            UserModel userModel=Helper.CookieManager.ReloadSessionFromCookie();
            if (userDetailsId!=null && userDetailsId != 0)
                userModel.UserDetailsID =(int)userDetailsId;
            UserProfile profile=facade.GetUserProfile(userModel);
            return View("UserProfilePartial", profile);
        }
        [HttpPost]
        public int EditUserProfile(FormCollection frm)
        {
            UserModel userModel = new UserModel();
            UserProfile userProfile = new UserProfile();
            TryUpdateModel(userProfile, frm);
            userModel = userProfile.user;
            BrandBookFacadeBiz facade = new BrandBookFacadeBiz();
            int result=0;
            try
            {
              result   = facade.EditUserProfile(userModel);
            }
            catch(Exception ex)
            {
                
            }
            return result;
            
        }
        public int inserBrand(FormCollection frm)
        {
            BrandModel userModel = new BrandModel();
            UserProfile userProfile = new UserProfile();
            TryUpdateModel(userProfile, frm);
            userModel = userProfile.brands;
            BrandBookFacadeBiz facade = new BrandBookFacadeBiz();
            int result = 0;
            try
            {
                result = facade.insertUserBrand(userModel,CookieManager.ReloadSessionFromCookie().UserDetailsID);
            }
            catch (Exception ex)
            {

            }
            return result;

        }

        public int inserProject(FormCollection frm)
        {
            ProjectModel userModel = new ProjectModel();
            UserProfile userProfile = new UserProfile();
            TryUpdateModel(userProfile, frm);
            userModel = userProfile.projects;
            BrandBookFacadeBiz facade = new BrandBookFacadeBiz();
            int result = 0;
            try
            {
                result = facade.insertUserProject(userModel, CookieManager.ReloadSessionFromCookie().UserDetailsID);
            }
            catch (Exception ex)
            {

            }
            return result;

        }

        /*
        public ContentResult GetBrandList()
        {
            BrandBookFacadeBiz facade = new BrandBookFacadeBiz();
            BrandModel brands = facade.GetBrandList(CookieManager.ReloadSessionFromCookie().UserDetailsID);
            return LargeJsonResult(brands);
        }
        public ContentResult GetProjectList()
        {
            BrandBookFacadeBiz facade = new BrandBookFacadeBiz();
            List<ProjectModel> projects = facade.GetProjectList(CookieManager.ReloadSessionFromCookie().UserDetailsID);
            return LargeJsonResult(projects);
        }
        public ContentResult LargeJsonResult(List<BrandModel> results)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue; // Whatever max length you want here  
            ContentResult result = new ContentResult();
            result.Content = serializer.Serialize(results);
            result.ContentType = "application/json";

            return result;
        }
        public ContentResult LargeJsonResult(List<ProjectModel> results)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue; // Whatever max length you want here  
            ContentResult result = new ContentResult();
            result.Content = serializer.Serialize(results);
            result.ContentType = "application/json";

            return result;
        }
          * */
    }
}
