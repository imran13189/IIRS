using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IIRS.Filters;
using IIRS.DAL;
using IIRS.Repository;

namespace IIRS.Controllers
{
    //[AdminFilter]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Files()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustomerFileOrders(int UserId)
        {
           return PartialView("_CustomerFileOrders",new ClerkRepository().GetCustomerFiles(UserId, null));
        }
    }
}