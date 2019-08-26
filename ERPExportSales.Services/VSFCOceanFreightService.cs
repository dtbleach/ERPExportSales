using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPExportSales.Entities;
using ERPExportSales.Repositories;

namespace ERPExportSales.Services
{
    public class VSFCOceanFreightService: IVSFCOceanFreightService
    {
        public IVSFCOceanFreightRepository iVSFCOceanFreightRepository;
        public VSFCOceanFreightService(IVSFCOceanFreightRepository iVSFCOceanFreightRepository)
        {
            this.iVSFCOceanFreightRepository = iVSFCOceanFreightRepository;
        }

        public IList<VSFCOceanFreight> GetOceanFreight()
        {
            try
            {
                return iVSFCOceanFreightRepository.GetAll().OrderBy(p => p.Continent).ToList();
            }catch(Exception ex)
            {
                return null;
            }
        }
    }
}
