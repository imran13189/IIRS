using IIRS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    public class DepartmentRepository : Connection
    {
        public List<GetDepartmentOrders_Result> GetOrders(int DepartmentUserId)
        {

            return _db.GetDepartmentOrders(DepartmentUserId).ToList();
        }

        public string GetComment(int commentId)
        {
           return _db.Comments.FirstOrDefault(x => x.CommentId == commentId).Comments;
        }
        public List<SP_GetComments_Result> GetComments(int userId,int orderId)
        {
            List<SP_GetComments_Result> commentList = _db.SP_GetComments(userId, orderId).ToList();
            return commentList;
        }
        public Order GetOrderDetails(int orderId)
        {
           return _db.Orders.FirstOrDefault(x => x.OrderId == orderId);
        }

        public bool AddComment(Comment comment)
        {
            comment.Created = DateTime.UtcNow;
            _db.Comments.Add(comment);
            _db.SaveChanges();
            return true;
        }
    }
}
