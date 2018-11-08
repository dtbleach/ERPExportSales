using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPExportSales.Web.Infrastructure
{
    public class ValidationResult
    {
        public ValidationResult()
        {
        }

        public ValidationResult(string memeberName, string message)
        {
            MemberName = memeberName;
            Message = message;
        }

        public ValidationResult(string message)
        {
            Message = message;
        }

        public string MemberName { get; set; }
        public string Message { get; set; }
    }
}