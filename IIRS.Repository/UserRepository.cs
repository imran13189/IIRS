using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IIRS.DAL;
using IIRS.Core;
namespace IIRS.Repository
{
    public class UserRepository:Connection
    {
        public LoggedInUserDetails GetLoginDetails(string userName, string password)
        {
           User user= _db.Users.FirstOrDefault(x => x.Username == userName && x.Password == password);
            if (user != null)
            {
                LoggedInUserDetails userDetails = new LoggedInUserDetails();
                userDetails.UserID = user.UserId;
                userDetails.Username = user.Username;
                userDetails.userInRole = user.UserInRoles.ToList();
                return userDetails;
            }
            else
            {
                return null;
            }

        }
    }
}
