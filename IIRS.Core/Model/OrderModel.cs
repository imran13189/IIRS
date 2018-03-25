using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIRS.Core.Model
{
    public partial class OrderModel
    {
        public long OrderId { get; set; }
        public int FileId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public System.DateTime Created { get; set; }
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public string CustomerName { get; set; }
        public int ApplicantUserId { get; set; }
        public int? DepartmentUserId { get; set; }
    }
}
