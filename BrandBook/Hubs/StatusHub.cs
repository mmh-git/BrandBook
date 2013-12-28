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
    public class StatusHub:Hub
    {
        public void GetNewStatus(JsonResult result)
        {
            
            StatusController statusController = new StatusController();
            Clients.All.GetNewStatus(result);
        }
    }
}