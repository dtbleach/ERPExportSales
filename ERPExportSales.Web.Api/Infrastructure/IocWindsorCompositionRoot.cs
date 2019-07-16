using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace ERPExportSales.Web.Api.Infrastructure
{
    public class IocWindsorCompositionRoot : IHttpControllerActivator
    {
        private readonly IWindsorContainer container;

        public IocWindsorCompositionRoot(IWindsorContainer container)
        {
            this.container = container;
        }

        public IHttpController Create(
        HttpRequestMessage request,
        HttpControllerDescriptor controllerDescriptor,
        Type controllerType)
        {
            var controller = (IHttpController)this.container.Resolve(controllerType);
            request.RegisterForDispose(new Release(() => this.container.Release(controller)));

            return controller;
        }

        private class Release : IDisposable
        {
            private readonly Action release;

            public Release(Action release)
            {
                this.release = release;
            }

            public void Dispose()
            {
                this.release();
            }
        }
    }
}