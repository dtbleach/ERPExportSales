using ERPExportSales.Web.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ERPExportSales.Web.Api.Filter
{
    public class WebApiInfoFilterAttribute : ActionFilterAttribute
    {
        Stopwatch stop = null;
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            stop = new Stopwatch();
            stop.Start(); 
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            try
            {
                stop.Stop();
                long milliseconds = stop.ElapsedMilliseconds;

                int? nullId = null;

                LoggerHelper.logHelper.Write(new SysLogInfo()
                {
                    ExecutionDuration = milliseconds,
                    ClientIpAddress = Net.GetIPAddress,
                    ClientName = Net.GetIPAddress,
                    MethodName = filterContext.Request.RequestUri.AbsoluteUri,
                    ServiceName = filterContext.ActionContext.ControllerContext.Controller.GetType().ToString(),
                    ExceptionMessage = filterContext.Exception?.Message,
                    Exception = filterContext.Exception?.StackTrace,
                    Parameters = filterContext.Request.Content.ReadAsStringAsync().Result,
                    Source="api"
                });
            }
            catch (Exception ex)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                byte[] myByte = Encoding.UTF8.GetBytes(ex.Message + ex.StackTrace);
                using (FileStream fsWrite = new FileStream(path + "/error.txt", FileMode.Append))
                {
                    fsWrite.Write(myByte, 0, myByte.Length);
                }
            }
            finally
            {
                base.OnActionExecuted(filterContext);
            }
        }
    }
}