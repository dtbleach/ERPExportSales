using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPExportSales.Web.Api.Models
{
    public class ClientHelper
    {
        private static ClientHelper clientHelper = null;
        // 默认索引
        public static string DEFAULT_INDEX = "crawlerdb";
        private ElasticClient Client()
        {
            var nodes = new Uri[]
            {
                 new Uri("http://192.168.1.133:9200"),
                 new Uri("http://192.168.1.135:9200")
            };
            var pool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(pool)
                .DefaultIndex(DEFAULT_INDEX)
                .PrettyJson();
            //.BasicAuthentication("elastic", "changeme");

            return new ElasticClient(settings);
        }

        public static ElasticClient getInstance()
        {
            if (clientHelper == null)
            {
                clientHelper = new ClientHelper();
            }
            return clientHelper.Client();
        }
    }
}