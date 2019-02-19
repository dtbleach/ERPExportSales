using ERPExportSales.Web.Api.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ERPExportSales.Web.Api.Controllers
{
    public class SearchController : ApiController
    {
        [System.Web.Http.Authorize]
        // GET: api/Search/
        public List<Resource> Get(string id,int from)
        {
            var client = ClientHelper.getInstance();
            var modList = client.Search<Resource>(s => s          
                .Query(q => q.MultiMatch(m => m.Fields(fd => fd.Fields(f => f.Keyword))
                                .Query(id)))
                                .From(from)
                                .Size(25));
            return modList.Hits.Select(p=>p.Source).ToList();
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