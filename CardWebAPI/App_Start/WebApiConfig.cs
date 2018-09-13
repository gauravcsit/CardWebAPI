using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CardWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{CardNumber},{ExpiryDate}",
                defaults: new { CardNumber = RouteParameter.Optional, ExpiryDate = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi1",
                routeTemplate: "api/{controller}/{action}",
                defaults: null
            );
        }
    }
}
