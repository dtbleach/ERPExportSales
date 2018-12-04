using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPExportSales.Entities;
using ERPExportSales.Repositories;

namespace ERPExportSales.Services
{
    public class ChartService : IChartService
    {
        public IQ195Repository iQ195Repository;

        public IUSDCNYRepository iUSDCNYRepository;
        public ChartService(IQ195Repository iQ195Repository, IUSDCNYRepository iUSDCNYRepository)
        {
            this.iQ195Repository = iQ195Repository;
            this.iUSDCNYRepository = iUSDCNYRepository;
        }
        public IList<Q195> GetAllQ195()
        {
            var q195List = iQ195Repository.GetAll();
            return q195List != null ? q195List.ToList() : null;
        }

        public IList<USDCNY> GetAllUSDCNY()
        {
            var usdcnyList = iUSDCNYRepository.GetAll();
            return usdcnyList != null ? usdcnyList.ToList() : null;
        }
    }
}
