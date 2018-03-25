using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IIRS.Filters;
using IIRS.Repository;
using IIRS.DAL;
using IIRS.Core;
using IIRS.Core.Model;

namespace IIRS.Controllers
{
    [ClerkFilter]
    public class ClerkController : Controller
    {
        // GET: Clerk
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Customer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendOrder(OrderModel order)
        {
            ClerkRepository _rep = new ClerkRepository();
             order.UserId = IIRS.Common.SessionManager.LoggedInUser.UserID;
            _rep.SendOrder(order);
            return Json(true);
        }

    }
}