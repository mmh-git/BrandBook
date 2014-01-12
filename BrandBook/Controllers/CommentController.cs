using BrandBookBiz;
using BrandBookModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrandBook.Helper;
using Microsoft.AspNet.SignalR;
using BrandBook.Hubs;
namespace BrandBook.Controllers
{
    public class CommentController : Controller
    {
        //
        // GET: /Comment/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void SaveComment(CommentModel commentModel)
        {
            BrandBookFacadeBiz _biz = new BrandBookFacadeBiz();
            
            commentModel = _biz.SaveComment(commentModel);
           
            string viewString = PartialView("CommentPartial", commentModel).Capture(this.ControllerContext);
            var result = new JsonResult
            {

                Data = new
                {
                    html = viewString,
                    commentModel = commentModel
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            var broadcastComment = GlobalHost.ConnectionManager.GetHubContext<CommentHub>();
            broadcastComment.Clients.All.GetComment(result);
        }
    }
}
