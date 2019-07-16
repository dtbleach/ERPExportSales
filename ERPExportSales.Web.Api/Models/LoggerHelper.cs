using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Web.Api.Models
{
    public class LoggerHelper
    {
        public static string configFilePath = AppDomain.CurrentDomain.BaseDirectory + @"log4net.config";
        public static LoggerHelper logHelper = new LoggerHelper();
        public LoggerHelper()
        {
            XmlConfigurator.Configure();
        }
        public bool Write(SysLogInfo logInfo)
        {
            //自定义添加的属性
            string propertiesMemberId = "MemberId";
            string propertiesServiceName = "ServiceName";
            string propertiesMethodName = "MethodName";
            string propertiesParameters = "Parameters";
            string propertiesClientIpAddress = "ClientIpAddress";
            string propertiesClientName = "ClientName";
            string propertiesBrowserInfo = "BrowserInfo";
            string propertiesException = "Exception";
            string propertiesExceptionMessage = "ExceptionMessage";
            string propertiesCustomData = "CustomData";
            string propertiesExecutionDuration = "ExecutionDuration";
            string propertiesSource = "Source";
            string pathlog4net = configFilePath;
            XmlConfigurator.Configure(new FileInfo(pathlog4net));
            try
            {
                GlobalContext.Properties[propertiesMemberId] = logInfo.MemberId;
                GlobalContext.Properties[propertiesServiceName] = logInfo.ServiceName;
                GlobalContext.Properties[propertiesMethodName] = logInfo.MethodName;
                GlobalContext.Properties[propertiesParameters] = logInfo.Parameters;
                GlobalContext.Properties[propertiesClientIpAddress] = logInfo.ClientIpAddress;
                GlobalContext.Properties[propertiesClientName] = logInfo.ClientName;
                GlobalContext.Properties[propertiesBrowserInfo] = logInfo.BrowserInfo;
                GlobalContext.Properties[propertiesCustomData] = logInfo.CustomData;
                GlobalContext.Properties[propertiesExecutionDuration] = logInfo.ExecutionDuration;
                GlobalContext.Properties[propertiesException] = logInfo.Exception == null ? "" : logInfo.Exception;
                GlobalContext.Properties[propertiesExceptionMessage] = logInfo.ExceptionMessage == null ? "" : logInfo.ExceptionMessage;
                GlobalContext.Properties[propertiesSource] = logInfo.Source;
                if (string.IsNullOrWhiteSpace(logInfo.ExceptionMessage))
                {
                    LogEncapsulation.Info(logInfo);
                }
                else
                {
                    LogEncapsulation.Error(logInfo);
                }
                return true;
            }
            catch (Exception e)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                byte[] myByte = System.Text.Encoding.UTF8.GetBytes(e.Message);
                using (FileStream fsWrite = new FileStream(path + "/errer.txt", FileMode.Append))
                {
                    fsWrite.Write(myByte, 0, myByte.Length);
                }
                return false;
            }
        }

    }
}
