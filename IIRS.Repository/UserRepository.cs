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
                userDetails.Name = user.FullName;
                return userDetails;
            }
            else
            {
                return null;
            }

        }
        public IEnumerable<Designation> GetDesig()
        {
            return _db.Designations;
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _db.Departments;
        }

        public bool Register(User user)
        {
           if(_db.Users.Any(x => x.Username == user.Username))
            {
                return true;
            }
            user.Created = DateTime.UtcNow;
            _db.Users.Add(user);
       
            _db.UserInRoles.Add(new UserInRole() { RoleId = 4, UserId = user.UserId });
            _db.SaveChanges();
            return false;

        }

        public User GetUserDetails(int userId)
        {
           return _db.Users.FirstOrDefault(x => x.UserId == userId);

        }
    }
}
