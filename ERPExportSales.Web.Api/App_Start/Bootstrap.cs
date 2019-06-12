using Castle.Windsor;
using Castle.Windsor.Installer;
using ERPExportSales.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPExportSales.Web.Api
{
    public class Bootstrap
    {
        private static IWindsorContainer _container;
      
        public static IWindsorContainer IocContainer
        {
            get
            {
                if(_container==null)
                    _container = new WindsorContainer().Install(FromAssembly.This());
                return _container;
            }
          
        }
    }
}