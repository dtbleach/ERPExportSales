using ERPExportSales.Crawler.Web.Models;
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
            string token = GetAPIToken(url);
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

        private string GetAPIToken(string url)
        {
            string token = string.Empty;
            string user = ConfigurationManager.AppSettings["CrawlerAPIUser"].ToString();
            string pwd = ConfigurationManager.AppSettings["CrawlerAPIPwd"].ToString();
            using (var db = new CrawlerDB())
            {
                bool flag = db.API访问令牌.Any(p => p.username == user && p.password == pwd);
                if (!flag)
                {
                    string content = "username=" + user + "&password=" + pwd + "&grant_type=password";
                    HttpContent httpContent = new StringContent(content);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                    using (var httpClient = new HttpClient())
                    {
                        HttpResponseMessage message = null;
                        var task = httpClient.PostAsync(url + "token", httpContent);
                        message = task.Result;
                        if (message != null && message.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            using (message)
                            {
                                string result = message.Content.ReadAsStringAsync().Result;
                                if (result.Length > 0)
                                {
                                    JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                                    API访问令牌 model = new API访问令牌();
                                    model.username = user;
                                    model.password = pwd;
                                    model.token = jo["access_token"].ToString();
                                    model.refresh_token = jo["refresh_token"].ToString();
                                    int time = 0;
                                    int.TryParse(jo["expires_in"].ToString(), out time);
                                    if (time > 0)
                                    {
                                        time = time + 1;
                                        if (time >= 86400) //天,
                                        {
                                            int day = Convert.ToInt16(time / 86400);
                                            model.expire = DateTime.Now.AddDays(day);
                                        }
                                    }
                                    db.API访问令牌.Add(model);
                                    db.SaveChanges();
                                    token = model.token;
                                }
                            }
                        }
                    }
                }
                else
                {
                    var model = db.API访问令牌.Where(p => p.username == user && p.password == pwd).FirstOrDefault();
                    if (model != null)
                    {
                        if (model.expire < DateTime.Now)
                        {
                            string content = "refresh_token=" + model.refresh_token + "&grant_type=refresh_token";
                            HttpContent httpContent = new StringContent(content);
                            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                            using (var httpClient = new HttpClient())
                            {
                                HttpResponseMessage message = null;
                                var task = httpClient.PostAsync(url + "token", httpContent);
                                message = task.Result;

                                if (message != null && message.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    using (message)
                                    {
                                        string result = message.Content.ReadAsStringAsync().Result;
                                        if (result.Length > 0)
                                        {
                                            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                                            model.token = jo["access_token"].ToString();
                                            model.refresh_token = jo["refresh_token"].ToString();
                                            int time = 0;
                                            int.TryParse(jo["expires_in"].ToString(), out time);
                                            if (time > 0)
                                            {
                                                time = time + 1;
                                                if (time >= 86400) //天,
                                                {
                                                    int day = Convert.ToInt16(time / 86400);
                                                    model.expire = DateTime.Now.AddDays(day);
                                                }
                                            }
                                            db.Entry(model).State = EntityState.Modified;
                                            db.SaveChanges();
                                            token = model.token;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            token = model.token;
                        }
                    }
                }
            }
            return token;
        }
    }


}