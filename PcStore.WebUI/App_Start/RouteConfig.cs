using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PcStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: null,
              url: "{specilization}/Page{page}",
              defaults: new { controller = "Pc", action = "List" },
              constraints: new { page = @"\d+" }
          );

            routes.MapRoute(
               name: null,
               url: "ProductListPage{page}",
               defaults: new { controller = "Pc", action = "List", specilization = (string)null, page = 1 }
           );

            routes.MapRoute(
               name: null,
               url: "{specilization}",
               defaults: new { controller = "Pc", action = "List", page = 1 }
           );

            routes.MapRoute(
               name: null,
               url: "",
               defaults: new { controller = "Pc", action = "List",specilization=(string)null,page=1 }
           );

            routes.MapRoute(
               name: null,
               url: "ProductListPage{page}",
               defaults: new { controller = "Pc", action = "List"}
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional }
            );
        }
    }
}
