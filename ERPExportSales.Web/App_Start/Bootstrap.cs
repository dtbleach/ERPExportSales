using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPExportSales.Web.App_Start
{
    public class Bootstrap
    {
        private static IWindsorContainer _container;
        private static IOpsPortalConfiguration _configuration;

        public static void Run()
        {
            _container = new WindsorContainer().Install(FromAssembly.This());

            _configuration = _container.Resolve<IOpsPortalConfiguration>();
        }

        public static IWindsorContainer IocContainer
        {
            get
            {
                return _container;
            }
            internal set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("IocContainer");
                }
                _container = value;
            }
        }

        public static IOpsPortalConfiguration ServiceConfiguration
        {
            get
            {
                return _configuration;
            }
        }
    }
}