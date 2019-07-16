
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ERPExportSales.Web.Api.Models
{
    public class LogEncapsulation
    {
        public static LogEncapsulation logEncapsulation = new LogEncapsulation();
        static LogEncapsulation()
        {
            string path = LoggerHelper.configFilePath;
            log4net.Config.XmlConfigurator.Configure(new FileInfo(path));
        }
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void Debug(string message)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }
        public static void Debug(System.Exception ex1)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(ex1.Message.ToString() + ex1.Source.ToString() + ex1.TargetSite.ToString() + ex1.StackTrace.ToString());
            }
        }
        public static void Error(Object message)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }
        public static void Fatal(Object message)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
        }
        public static void Info(Object message)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }

        public static void Warn(Object message)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }
    }

}