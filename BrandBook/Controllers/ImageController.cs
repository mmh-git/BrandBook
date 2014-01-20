using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrandBook.Controllers
{
    public class ImageController : Controller
    {
        //
        // GET: /Image/

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SaveImage(IEnumerable<HttpPostedFileBase> files)
        {
            var fileName = "";
            var physicalPath="";
            foreach (var file in files)
            {
                fileName = file.FileName;
                physicalPath = Path.Combine(Server.MapPath("~/Content/Images/UserImages"), fileName);
                file.SaveAs(physicalPath);
            }
            return new JsonResult { Data = new { response = Url.Content("~/Content/Images/UserImages/" + fileName), JsonRequestBehavior.AllowGet } };
        }
    }
}
