using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace ERPExportSales.Web.Api.Models
{
    public class MyJsonResult : JsonResult
    {
        public MyJsonResult(object data)
        {
            Data = data;
        }
        public object Data { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var json = JsonConvert.SerializeObject(Data,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),        //小驼峰命名法
                    DateFormatString = "yyyy-MM-dd HH:mm:ss"
                }
                );
            context.HttpContext.Response.Write(json);
        }
    }

}