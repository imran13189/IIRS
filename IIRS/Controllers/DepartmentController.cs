using IIRS.Core.Model;
using IIRS.DAL;
using IIRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IIRS.Common;
namespace IIRS.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
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
            return PartialView("_CustomerFileOrders", new ClerkRepository().GetCustomerFiles(UserId, null));
        }

        public ActionResult DepartmentOrders(int orderId)
        {
            UserRepository _repo = new UserRepository();
            ViewBag.Desig = _repo.GetDesig();
            ViewBag.Department = _repo.GetDepartments();
            DepartmentRepository _deptRepo = new DepartmentRepository();
            Order order=  _deptRepo.GetOrderDetails(orderId);
            return View(new OrderModel() {OrderId=order.OrderId, ApplicantUserId = order.ApplicantUserId,DepartmentUserId=order.DepartmentUserId, CustomerName = order.User.FullName});
        }

        [HttpPost]
        public JsonResult AddComment(Comment comment)
        {
            comment.UserId = SessionManager.LoggedInUser.UserID;
            DepartmentRepository _dept = new DepartmentRepository();
            _dept.AddComment(comment);
            return Json(true);
        }
    }
}