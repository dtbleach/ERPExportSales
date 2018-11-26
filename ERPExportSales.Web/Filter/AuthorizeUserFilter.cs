using ERPExportSales.Core;
using ERPExportSales.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ERPExportSales.Services;
using System.Text;
using ERPExportSales.Entities;
using ERPExportSales.Web.Core;
using ERPExportSales.Web.Models;

namespace ERPExportSales.Web.Filter
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        private const string _securityToken = "token";

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Authorize(filterContext))
            {
                return;
            }

            filterContext.Result = new RedirectResult("/Account/Login");
            //HandleUnauthorizedRequest(filterContext);
        }

        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    base.HandleUnauthorizedRequest(filterContext);
        //}

        private bool Authorize(AuthorizationContext actionContext)
        {
            try
            {
                bool rememberLogin = false;
                bool.TryParse(CookieHelper.GetCookieValue("rememberLogin"), out rememberLogin);
                if (rememberLogin)
                {
                    HttpRequestBase request = actionContext.RequestContext.HttpContext.Request;
                    string token = CookieHelper.GetCookieValue(_securityToken);
                    return SecurityManager.VerifyToken(token, request);
                }else
                {
                    var user = SessionHelper.Get<UserViewModel>("User");
                    if (user != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}