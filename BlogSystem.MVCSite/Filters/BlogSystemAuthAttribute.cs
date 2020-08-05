using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogSystem.MVCSite.Filters
{
    public class BlogSystemAuthAttribute:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //当用户登录数据存储在Cookies中时，把Cookies里的登录信息同步到Session中
            if (filterContext.HttpContext.Session["loginName"] == null &&
                  filterContext.HttpContext.Request.Cookies["loginName"] != null)
            {
                filterContext.HttpContext.Session["loginName"] = filterContext.HttpContext.Request.Cookies["loginName"].Value;
                filterContext.HttpContext.Session["userid"] = filterContext.HttpContext.Request.Cookies["userid"].Value;

            }

            if (!(filterContext.HttpContext.Session["loginName"] != null ||
                filterContext.HttpContext.Request.Cookies["loginName"] != null))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                {
                    {"controller","Home"},
                    {"action","Login"}
                });
            }
        }
    }
}