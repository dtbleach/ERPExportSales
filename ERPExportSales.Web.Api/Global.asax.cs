using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using ERPExportSales.Web.Api.Infrastructure;
using ERPExportSales.Web.API.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ERPExportSales.Web.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;
        protected void Application_Start()
        {
            ConfigureWindsor(GlobalConfiguration.Configuration);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(c => WebApiConfig.Register(c, _container));
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
            // 记录日志
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(Server.MapPath("Log4Net.config")));
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // 返回当前异常
            var ex = Server.GetLastError();
            // 记录日志信息
            //Log4netHelper.Error(ex.Message, ex);
        }

        public static void ConfigureWindsor(HttpConfiguration configuration)
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel, true));
            var dependencyResolver = new IocWindsorDependencyResolver(_container);
            configuration.DependencyResolver = dependencyResolver;
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(_container.Kernel));
        }

        protected void Application_End()
        {
            _container.Dispose();
            base.Dispose();
        }
    }
}
