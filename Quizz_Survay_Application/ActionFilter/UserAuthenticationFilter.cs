using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.UI.WebControls;

namespace SignIn.ActionFilter
{
    public class UserAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        void IAuthenticationFilter.OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.HttpContext.Session["CurrUser"] == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                if((string)filterContext.HttpContext.Session["roletype"] == "Admin")
                {
                    RedirectResult r = new RedirectResult("/Admin/Index");
                }
                else
                {
                    RedirectResult r = new RedirectResult("/User/Index");
                }
            }
        }

        void IAuthenticationFilter.OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new ViewResult { ViewName = "Error" };
            }
        }
    }
}