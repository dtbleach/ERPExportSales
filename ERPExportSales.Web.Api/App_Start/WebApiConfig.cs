using Castle.Windsor;
using ERPExportSales.Web.Api.Infrastructure;
using ERPExportSales.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace ERPExportSales.Web.Api
{
    public static class WebApiConfig
    {
        //public static void Register(HttpConfiguration config)
        //{
        //    // Web API 配置和服务

        //    // Web API 路由
        //    config.MapHttpAttributeRoutes();

        //    config.Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "api/{controller}/{id}",
        //        defaults: new { id = RouteParameter.Optional }
        //    );
        //}
        public static void Register(HttpConfiguration config, IWindsorContainer container)
        {

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
            );

            //HttpClient请求内容压缩的支持
            config.MessageHandlers.Add(new DecompressionHandler());
            RegisterControllerActivator(container);
        }

        private static void RegisterControllerActivator(IWindsorContainer container)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator),
            new IocWindsorCompositionRoot(container));
        }
    }
}
