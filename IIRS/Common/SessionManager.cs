using IIRS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IIRS.DAL;

namespace IIRS.Common
{
    public class SessionManager
    {
        public static LoggedInUserDetails LoggedInUser
        {
            get
            {
                if (HttpContext.Current == null)
                    return null;
                if (HttpContext.Current.Session["LoggedInUser"] == null)
                {
                    HttpContext.Current.Session["LoggedInUser"] = new LoggedInUserDetails();
                }
                return (LoggedInUserDetails)HttpContext.Current.Session["LoggedInUser"];
            }
            set { HttpContext.Current.Session["LoggedInUser"] = value; }
        }
        public static void FillSession(int userid, string Username, string name, IList<UserInRole> userInRoles)
        {
            SessionManager.LoggedInUser.UserID = userid;
            SessionManager.LoggedInUser.Name = name;


            SessionManager.LoggedInUser.Username = Username;

            SessionManager.LoggedInUser.userInRole = userInRoles;


        }
    }
   
}