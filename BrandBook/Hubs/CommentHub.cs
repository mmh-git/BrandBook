using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Web.Mvc;
using BrandBookModel;
using BrandBook.Controllers;
using System.Web.Security;
namespace BrandBook.Hubs
{
    public class CommentHub:Hub
    {
        public void GetComment(JsonResult result)
        {
            Clients.All.GetComment(result);
        }
    }
}