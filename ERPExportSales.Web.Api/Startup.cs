﻿using System;
using System.Web.Http;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Castle.Windsor;
using Castle.Windsor.Installer;

[assembly: OwinStartup(typeof(ERPExportSales.Web.Api.Startup))]
namespace ERPExportSales.Web.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IWindsorContainer container = new WindsorContainer().Install(FromAssembly.This());
            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(app);
          //  WebApiConfig.Register(config, container);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider=new RefreshTokenProvider()
            };
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}