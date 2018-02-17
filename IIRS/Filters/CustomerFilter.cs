using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Routing;

namespace IIRS.Filters
{
    public class CustomerFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                //if (BuildersAlliances.Common.SessionManager.LoggedInUser.RoleId==null)
                {
                    if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                    {
                        HttpContext.Current.Response.Redirect("~/Account");
                    }
                }
                else
                {
                    if (IIRS.Common.SessionManager.LoggedInUser.userInRole.Count == 0)
                    {
                        if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                        {
                            HttpContext.Current.Response.Redirect("~/Account/Login");
                        }
                    }

                    else
                    {
                        if (IIRS.Common.SessionManager.LoggedInUser.userInRole.FirstOrDefault().Role.RoleName != "Customer")
                        {
                            HttpContext.Current.Response.Redirect("~/Account/Login"); ;
                        }
                    }
                }
            }
            catch
            {

            }
            
        }
    }
}