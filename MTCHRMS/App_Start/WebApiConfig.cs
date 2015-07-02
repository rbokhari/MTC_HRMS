using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
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
                name: "DefaultApiByAction",
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


            config.Formatters.JsonFormatter.SupportedMediaTypes         //tell if any accept : text/html is coming, then return json formatter
                .Add(new MediaTypeHeaderValue("text/html"));

            //config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();     // telling that xml return is not available

            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            //var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
