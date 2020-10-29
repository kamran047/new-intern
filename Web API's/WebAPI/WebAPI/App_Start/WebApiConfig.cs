using DependencyInjection;
using Newtonsoft.Json.Serialization;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiContrib.IoC.Ninject;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static StandardKernel Register(HttpConfiguration config)
        {
            config.Filters.Add(new AuthorizeAttribute());
            var kernel = new StandardKernel();
            Register(config, kernel);
            return kernel;
        }

        public static void Register(HttpConfiguration config, IKernel kernel)
        {
            // Web API configuration and services
            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();

            var modules = new List<INinjectModule>
            {
                    new NinjectBinding()
            };
            kernel.Load(modules);

            config.DependencyResolver = new NinjectResolver(kernel);
            config.Routes.MapHttpRoute(
                  name: "DefaultApi",
                  routeTemplate: "api/{controller}/{id}",
                  defaults: new { id = RouteParameter.Optional }
              );

            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Newtonsoft.Json.Formatting.Indented;
        }
    }
}
