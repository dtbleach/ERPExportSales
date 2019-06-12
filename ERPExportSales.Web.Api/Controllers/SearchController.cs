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

namespace ERPExportSales.Web.Api.Controllers
{
    public class SearchController : ApiController
    {
        [System.Web.Http.Authorize]
        // GET: api/Search/
        public string Get(string id,int from)
        {
            var client = ClientHelper.getInstance();
            string json = string.Empty;
            if (string.IsNullOrEmpty(id))
            {
                var modList = client.Search<Resource>(s => s
         .Query(q => q.MultiMatch(m => m.Fields(fd => fd.Fields(f => f.Keyword))
                         .Query(id)))
                         .From(from)
                         .Size(25));
                json = GetResutJson(modList);
            }
            else
            {
                var modList = client.Search<Resource>(s => s.Query(q => q.Bool(t => t.Must(m => m.Match(o => o.Field(f => f.Keyword).
                Query(id).Operator(Operator.And))
     ))).From(from).Size(25));
                json = GetResutJson(modList);
            }

            return json;  
        }

        
        [System.Web.Http.Authorize]
        [Route("api/search/chart")]
        [HttpPost]
        // GET: api/Search/
        public string Chart(string id)
        {
            var client = ClientHelper.getInstance();
            string json = string.Empty;
            if (string.IsNullOrEmpty(id))
            {
                var modList = client.Search<Resource>(s => s
         .Query(q => q.MultiMatch(m => m.Fields(fd => fd.Fields(f => f.Keyword))
                         .Query(id))));
                json = GetResutJson(modList);
            }
            else
            {
                var modList = client.Search<Resource>(s => s.Query(q => q.Bool(t => t.Must(m => m.Match(o => o.Field(f => f.Keyword).
                Query(id).Operator(Operator.And))
     ))));
                json = GetResutJson(modList);
            }

            return json;
        }

        private string GetResutJson(ISearchResponse<Resource> model)
        {        
            string jsonTxt = "{\"Total\":" + model.Total + ",\"Data\":";
            var list = model.Hits.Select(p => p.Source).ToList();
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