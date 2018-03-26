using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IIRS.Filters;
using IIRS.DAL;
using IIRS.Repository;
using IIRS.Core.Model;
using IIRS.Common;
namespace IIRS.Controllers
{
    [AdminFilter]
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

        [HttpPost]
        public ActionResult SendOrder(OrderModel order)
        {
            ClerkRepository _rep = new ClerkRepository();
            order.UserId = IIRS.Common.SessionManager.LoggedInUser.UserID;
            _rep.SendOrder(order);
            return Json(true);
        }


        public ActionResult SendToDepartment(int userId)
        {
            UserRepository _repo = new UserRepository();
            ViewBag.Desig = _repo.GetDesig();
            ViewBag.Department = _repo.GetDepartments();
            User user=  _repo.GetUserDetails(userId);
            return View(new OrderModel() { ApplicantUserId=user.UserId,CustomerName=user.FullName});
        }

        [HttpPost]
        public ActionResult SendToDepartment(OrderModel model)
        {
            AdminRepository _admin = new AdminRepository();
            model.UserId = SessionManager.LoggedInUser.UserID;
            if (_admin.SendOrder(model))
            {
                return RedirectToAction("Files");
            }
            else
            {
                UserRepository _repo = new UserRepository();
                ViewBag.Desig = _repo.GetDesig();
                ViewBag.Department = _repo.GetDepartments();

            }
            return View(model);
        }
    }
}