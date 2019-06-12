using ERPExportSales.Crawler.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ERPExportSales.Crawler.Web.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index(string id)
        {
            string url = ConfigurationManager.AppSettings["CrawlerAPIUrl"].ToString();
            string token = CommonHelper.GetAPIToken(url);
            string result = string.Empty;
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage message = null;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var task = httpClient.GetAsync(url + "api/search/chart?id=" + id);
                message = task.Result;

                if (message != null && message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    using (message)
                    {
                        result = message.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            return Json(result);
        }
    }
}