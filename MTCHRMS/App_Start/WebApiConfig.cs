using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace MTCHRMS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiByName",
                routeTemplate: "api/{controller}/{id}/{action}"
                //defaults: new {id = RouteParameter.Optional}
                );


            config.Routes.MapHttpRoute(
                name: "DefaultApiByUserName",
                routeTemplate: "api/{controller}/{action}"
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApiActionWithId",
                routeTemplate: "api/{controller}/{action}/{id}"
                //defaults: new { id = RouteParameter.Optional }
                );

            //var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
