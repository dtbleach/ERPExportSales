﻿using ERPExportSales.Crawler.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ERPExportSales.Crawler.Web.Controllers
{
    public class CrawlerController : Controller
    {
        // GET: Crawler
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public JsonResult GetContent(string id,int pageIndex)
        {
            string url = ConfigurationManager.AppSettings["CrawlerAPIUrl"].ToString();
            string token = CommonHelper.GetAPIToken(url);
            string result = string.Empty;
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage message = null;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var task = httpClient.GetAsync(url + "api/search?id="+ id + "&from="+pageIndex);
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