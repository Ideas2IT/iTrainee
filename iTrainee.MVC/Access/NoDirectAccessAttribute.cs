//using System;
//using System.Web.Mvc;
//using System.Web.Routing;


//namespace iTrainee.MVC.Access
//{
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//    public class NoDirectAccessAttribute : ActionFilterAttribute
//    {
//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            if (filterContext.HttpContext.Request.UrlReferrer == null ||
//                        filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host)
//            {
//                filterContext.Result = new RedirectToRouteResult(new
//                               Microsoft.AspNetCore.Routing.RouteValueDictionary(new { controller = "Home", action = "Index", area = "" }));
//            }
//        }
//    }
//}
