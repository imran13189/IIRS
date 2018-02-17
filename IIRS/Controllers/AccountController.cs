using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IIRS.DAL;
using IIRS.Core;
using IIRS.Repository;
using System.Web.Security;
using IIRS.Common;

namespace IIRS.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            UserRepository _repo = new UserRepository();
            LoggedInUserDetails data = _repo.GetLoginDetails(Username, Password);
           
            if (data != null)
            {
                SessionManager.FillSession(data.UserID, data.Username, data.Name, data.userInRole);
                FormsAuthentication.SetAuthCookie(Username, false);
                if(data.userInRole.FirstOrDefault(x=>x.Role.RoleName=="Admin")!=null)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (data.userInRole.FirstOrDefault(x => x.Role.RoleName == "Clerk") != null)
                {
                    return RedirectToAction("Index", "Clerk");
                }
                else if (data.userInRole.FirstOrDefault(x => x.Role.RoleName == "Customer") != null)
                {
                    return RedirectToAction("Index", "Customer");
                }
                return RedirectToAction("Login");
            }

            return RedirectToAction("Login");
        }
        public ActionResult LogOut()
        {
            //Session.Abandon();
            FormsAuthentication.SignOut();
            //System.Web.HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

            return RedirectToAction("Login", "Account");
        }
    }
}