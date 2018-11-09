using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPExportSales.Entities;
using ERPExportSales.Repositories;

namespace ERPExportSales.Services
{
    public class ExportSalesLoginTokenService : IExportSalesLoginTokenService
    {
        public IExportSalesLoginTokenRepository exportSalesLoginTokenRepository;
        public IUnitOfWork unitOfWork;
        public ExportSalesLoginTokenService(IUnitOfWork unitOfWork, IExportSalesLoginTokenRepository exportSalesLoginTokenRepository)
        {
            this.unitOfWork = unitOfWork;
            this.exportSalesLoginTokenRepository = exportSalesLoginTokenRepository;
        }
        public ExportSalesLoginToken GetExportSalesLoginToken(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                var loginToken = exportSalesLoginTokenRepository.Get(p => p.UserName == username);
                return loginToken;
            }
            return null;
        }

        public void SaveLoginToken(ExportSalesLoginToken model)
        {
            if (null != model)
            {
                var tokenModel = exportSalesLoginTokenRepository.Get(p => p.UserName == model.UserName);
                if (null != tokenModel)
                {
                    tokenModel.Token = model.Token;
                    tokenModel.ExpiresTime = model.ExpiresTime;
                    exportSalesLoginTokenRepository.Update(tokenModel);
                }
                else
                {
                    exportSalesLoginTokenRepository.Add(model);
                }
                unitOfWork.Commit();
            }
        }
    }
}
