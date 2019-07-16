
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ERPExportSales.Web.Api.Models
{
    public class SysLogInfo
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public int? MemberId { get; set; }
        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 方法名
        /// </summary> 
        public string MethodName { get; set; }
        /// <summary>
        /// 耗时
        /// </summary>
        public long ExecutionDuration { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Parameters { get; set; }
        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public string ClientIpAddress { get; set; }
        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 设备信息
        /// </summary>
        public string BrowserInfo { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Exception { get; set; }
        /// <summary>
        /// 异常描述
        /// </summary>
        public string ExceptionMessage { get; set; }
        /// <summary>
        /// 客户数据
        /// </summary>
        public string CustomData { get; set; }

        public string Source { get; set; }
    }
}