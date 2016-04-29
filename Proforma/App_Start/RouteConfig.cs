using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Proforma
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "CompByCat",
            //    url: "{controller}/{action}/{id}/{FilterCriteriaId}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //    routes.MapRoute(
            //    name: "ApiById",
            //    url: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional },
            //    constraints: new { id = @"^[0-9]+$" }
            //);

            //    routes.MapRoute(
            //        name: "ApiByName",
            //        url: "api/{controller}/{action}/{name}",
            //        defaults: null,
            //        constraints: new { name = @"^[a-z]+$" }
            //    );

            //routes.MapRoute(
            //    name: "ApiByAction",
            //    url: "api/{controller}/{action}",
            //    defaults: new { id = RouteParameter.Optional }
            //);



            //routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);


        }
    }
}
