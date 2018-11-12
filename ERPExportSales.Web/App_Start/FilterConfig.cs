using ERPExportSales.Web.Filter;
using System.Web;
using System.Web.Mvc;

namespace ERPExportSales.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TrackerFilter());
            filters.Add(new CustomErrorAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
