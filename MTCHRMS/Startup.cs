using Microsoft.Owin;
using MTCHRMS.App_Start;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApiContrib.IoC.Ninject;

[assembly: OwinStartupAttribute(typeof(MTCHRMS.Startup))]
namespace MTCHRMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.DependencyResolver = new NinjectResolver(NinjectConfig.CreateKernel());

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

        }


    }
}
