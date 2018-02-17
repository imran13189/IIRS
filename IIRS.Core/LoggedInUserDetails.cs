using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IIRS.DAL;

namespace IIRS.Core
{
    public  class LoggedInUserDetails
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public IList<UserInRole> userInRole { get; set; }
    }
}
