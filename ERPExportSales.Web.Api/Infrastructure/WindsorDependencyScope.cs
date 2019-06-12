using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace ERPExportSales.Web.API.Infrastructure
{
    public class WindsorDependencyScope : IDependencyScope
    {
        private readonly IKernel _container;
        private readonly IDisposable _scope;

        public WindsorDependencyScope(IKernel container)
        {
            _container = container;
            _scope = container.BeginScope();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }

        public object GetService(Type serviceType)
        {
            var service = _container.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
            if (service == null)
            {
                // TODO :  log or error handle
            }

            return service;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.ResolveAll(serviceType).Cast<object>();
        }
    }
}