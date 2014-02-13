using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrandBookModel;
using BrandBookBiz;
using System.Web.Security;
using BrandBook.Hubs;
using Microsoft.AspNet.SignalR;
using BrandBook.Helper;

namespace BrandBook.Controllers
{
    public class LikeController : Controller
    {
        //
        // GET: /Like/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void SaveLike(LikeModel likeModel)
        {
            UserModel currentLoggedInUser = CookieManager.ReloadSessionFromCookie();
            likeModel.LikedByUserID = currentLoggedInUser.UserDetailsID;
            BrandBookFacadeBiz facade = new BrandBookFacadeBiz();
            List<LikeModel> likes = facade.SaveLike(likeModel);
            var broadCastLikeMessage = GlobalHost.ConnectionManager.GetHubContext<LikeHub>();
            LikeModel temp = likes.Find(t => t.LikedByUserID == likeModel.LikedByUserID);
            likeModel.LikeID = temp != null ? temp.LikeID : 0;
            var result = new JsonResult
            {

                Data = new
                {
                    likes = likes,
                    like=likeModel
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            broadCastLikeMessage.Clients.All.SaveLikeCallBack(result);

            
            //return Json(likeModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLikedUserCollection(int LikedContentId)
        {
            BrandBookFacadeBiz facade = new BrandBookFacadeBiz();

            return View("GetLikedUserPartial", facade.GetLikedUserCollection(LikedContentId));
        }

    }
}