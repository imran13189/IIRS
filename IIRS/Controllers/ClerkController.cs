using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IIRS.Filters;
namespace IIRS.Controllers
{
    //[ClerkFilter]
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


    }
}