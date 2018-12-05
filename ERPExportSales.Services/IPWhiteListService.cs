using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPExportSales.Entities;
using ERPExportSales.Repositories;

namespace ERPExportSales.Services
{
    public class IPWhiteListService : IIPWhiteListService
    {
        public IIPWhiteListRepository ipWhiteListRepository;
        public IPWhiteListService(IIPWhiteListRepository ipWhiteListRepository)
        {
            this.ipWhiteListRepository = ipWhiteListRepository;
        }
        public IList<IPWhiteList> GetIPWhiteList()
        {
            var ipWhiteList = ipWhiteListRepository.GetAll();
            return ipWhiteList != null ? ipWhiteList.ToList() : null;
        }

        public bool IsIPExistWhiteList(string ip)
        {
            if (ip.Equals("::1"))
            {
                return true;
            }

            int i = ip.LastIndexOf(".");
            ip = ip.Substring(0, ip.Length - i);
            var whiteList = GetIPWhiteList();
            if (whiteList != null && whiteList.Count() > 0)
            {
                var ipList = whiteList.Select(p => p.IP).ToList();
                if (ipList.Contains(ip))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
