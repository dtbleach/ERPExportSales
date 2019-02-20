using ERPExportSales.Web.Api.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ERPExportSales.Web.Api.Controllers
{
    public class SearchController : ApiController
    {
        [System.Web.Http.Authorize]
        // GET: api/Search/
        public string Get(string id,int from)
        {
            var client = ClientHelper.getInstance();
            var modList = client.Search<Resource>(s => s          
                .Query(q => q.MultiMatch(m => m.Fields(fd => fd.Fields(f => f.Keyword))
                                .Query(id)))
                                .From(from)
                                .Size(25));
            string jsonTxt = "{\"Total\":" + modList.Total + ",\"Data\":";
            var list = modList.Hits.Select(p => p.Source).ToList();
            DataContractJsonSerializer json = new DataContractJsonSerializer(list.GetType());
            string szJson = "";
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, list);
                szJson = Encoding.UTF8.GetString(stream.ToArray());
            }
            jsonTxt = jsonTxt + szJson + "}";
            return jsonTxt;
        }
        [System.Web.Http.Authorize]
        public long Get(string id)
        {
            var client = ClientHelper.getInstance();
            var modList = client.Search<Resource>(s => s
                .Query(q => q.MultiMatch(m => m.Fields(fd => fd.Fields(f => f.Keyword))
                                .Query(id))));
            return modList.Total;
        }

    }
}