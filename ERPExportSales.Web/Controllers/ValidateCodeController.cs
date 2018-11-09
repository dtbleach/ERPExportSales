using ERPExportSales.Framework;
using ERPExportSales.Web.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPExportSales.Web.Controllers
{
    public class ValidateCodeController : Controller
    {
        // GET: ValidateCode
        public ActionResult GetValidateCode()
        {
            ValidatedCode v = new ValidatedCode();
            string code = v.CreateVerifyCode();            //取随机码  
            var data=v.CreateImageOnPage(code);       // 输出图片  
            SessionHelper.Add("CheckCode", code.ToLower());
            //Session["CheckCode"] = code.ToLower();                   //Session 取出验证码  
            return File(data, "image/jpeg");
        }
    }
}