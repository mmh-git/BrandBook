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
    public class StatusController : Controller
    {
        //
        // GET: /Status/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetStatusList()
        {
            BrandBookFacadeBiz _bizContext = new BrandBookFacadeBiz();
            StatusUpdateModel statusUpdateModel = new StatusUpdateModel();
            List<StatusUpdateModel>statusList= _bizContext.GetStatusList(statusUpdateModel);
            return View(statusList);
        }
        public void SaveStatus(StatusUpdateModel statusUpdateModel)
        {
            BrandBookFacadeBiz _bizContext = new BrandBookFacadeBiz();
            statusUpdateModel = _bizContext.SaveStatus(statusUpdateModel);
            statusUpdateModel.Comments = new List<CommentModel>();
            statusUpdateModel.Likes = new List<LikeModel>();
            List<StatusUpdateModel> statusList = new List<StatusUpdateModel>();
            statusList.Add(statusUpdateModel);
            
            var viewString = View("GetStatusList", statusList).Capture(this.ControllerContext);
            var result= new JsonResult
            {
                
                Data = new { 
                html=viewString
                },
                JsonRequestBehavior=JsonRequestBehavior.AllowGet
            };
            var broadcastStatus = GlobalHost.ConnectionManager.GetHubContext<StatusHub>();
            broadcastStatus.Clients.All.GetNewStatus(result);
        }
    }
}
