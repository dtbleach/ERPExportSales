using Castle.MicroKernel;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http.Dependencies;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace ERPExportSales.Web.API.Infrastructure
{
    public class WindsorDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IKernel _container;

        public WindsorDependencyResolver(IKernel container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(_container);
        }

        public object GetService(Type serviceType)
        {
            return _container.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose() { }
    }

}