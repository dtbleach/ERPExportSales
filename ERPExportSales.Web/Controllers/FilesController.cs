using ERPExportSales.Configuration;
using ERPExportSales.Framework;
using ERPExportSales.Web.Filter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPExportSales.Web.Controllers
{
    public class FilesController : Controller
    {
        public IExportSalesConfiguration config;
        public FilesController(IExportSalesConfiguration config)
        {
            this.config = config;
        }

        [AuthorizeUser]
        public ActionResult GetPDF(string file)
        {
            try
            {
                string fileName = string.Empty;
                string folder = string.Empty;
                if (!string.IsNullOrEmpty(file))
                {
                    file = Encryption64.Decrypt(file, config.Encryption64Key);
                    string[] urlParam = file.Split(':');
                    fileName = urlParam[0];
                    folder = urlParam[1];
                }
                ExportSalesWebService.SFCServiceSoapClient client = new ExportSalesWebService.SFCServiceSoapClient();
                fileName = fileName + ".pdf";
                var dfile = client.下载附件("", "外销", folder, fileName, "", "");
                string dirp = System.Web.HttpContext.Current.Server.MapPath("~/Files/" + folder);
                DirectoryInfo mydir = new DirectoryInfo(dirp);
                string fullPath = dirp + "\\" + fileName;
                FileStream writeStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                MemoryStream readStream = new MemoryStream(dfile);
                int Length = dfile.Length;
                int bytesRead = readStream.Read(dfile, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(dfile, 0, bytesRead);
                    bytesRead = readStream.Read(dfile, 0, Length);
                }
                readStream.Close();
                writeStream.Close();
                return new FilePathResult(fullPath, "application/pdf");
            }catch(Exception ex)
            {
                LoggerHelper.Error("预览PDF出错", ex);
                return null;
            }
        }


        [AuthorizeUser]
        public ActionResult PDF(string file)
        {
            var request = HttpContext.Request;
            string host = request.Url.Authority;
            ViewBag.FullPath = "http://"+host+"/Files/GetPDF?file="+ System.Web.HttpUtility.UrlEncode(file);
            return View();
        }


        private void DeleteTempPdfFile(string fullpath)
        {
            string[] files = Directory.GetFiles(fullpath, "*.pdf");
            FileInfo fi;
            foreach (var file in files)
            {
                fi = new FileInfo(file);
                fi.Delete();
            }
        }
    }
}