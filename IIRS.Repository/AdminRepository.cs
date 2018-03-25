using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IIRS.DAL;
using IIRS.Core.Model;

namespace IIRS.Repository
{
    public class AdminRepository:Connection
    {
        private object designationModel;

        public List<GetAdminFiles_Result> GetFiles(string search)
        {
            return _db.GetAdminFiles(search).ToList();
        }
        public bool SaveOrder(OrderModel model)
        {
             _db.Orders.Add(new Order() {
                 ApplicantUserId=model.UserId,
                 Created=DateTime.UtcNow,
                 DepartmentUserId=model.DesignationId
             });
            _db.SaveChanges();
            return true;
        }

        public List<DesignationModel> GetDesignation(int DepartmentId)
        {
            List<Designation>  designation= _db.Designations.Where(x => x.DepartmentId == DepartmentId).ToList();
            List<DesignationModel> designationModel = new List<DesignationModel>();
            foreach(var item in designation)
            {
                designationModel.Add(new DesignationModel() {
                    DesignationId=item.DesignationId,
                    DesignationName=item.DesignationName
                });
            }
            return designationModel;
        }

        public List<UserModel> GetUser(int DesignationId)
        {
            IEnumerable<User> designation = _db.Users.Where(x => x.DesignationId == DesignationId).ToList();
            List<UserModel> userModel = new List<UserModel>();
            foreach (var item in designation)
            {
                userModel.Add(new UserModel()
                {
                    UserId = item.UserId,
                    FullName = item.FullName
                });
            }
            return userModel;
        }


        public bool SendOrder(OrderModel order)
        {
            Order orderDetails = _db.Orders.FirstOrDefault(x => x.ApplicantUserId == order.ApplicantUserId);
            if(!orderDetails.DepartmentUserId.HasValue)
            orderDetails.DepartmentUserId = order.DepartmentUserId;
           
            Comment comment = new Comment();
            comment.UserId = order.UserId;
            comment.Comments = order.Comment;
            comment.Created = DateTime.UtcNow;
            comment.OrderId = orderDetails.OrderId;
            _db.Comments.Add(comment);
            _db.SaveChanges();
            return true;
        }
    }
}
