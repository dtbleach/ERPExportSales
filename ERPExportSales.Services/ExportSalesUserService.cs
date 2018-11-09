using ERPExportSales.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Services
{
    public class ExportSalesUserService :IExportSalesUserService
    {
        public IDatabaseFactory databaseFactory;

        public IUnitOfWork unitOfWork;

        public ExportSalesUserService(IDatabaseFactory databaseFactory,IUnitOfWork unitOfWork)
		{
            this.databaseFactory = databaseFactory;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 外销用户登陆校验
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public BizResult<bool> Login(string loginName, string password)
        {
            BizResult<bool> result = new BizResult<bool>();

            if (string.IsNullOrEmpty(loginName))
            {
                result.Result = false;
                result.Message = "用户名为空";
                return result;
            }

            if (string.IsNullOrEmpty(password))
            {
                result.Result = false;
                result.Message = "密码为空";
                return result;
            }

            var db = databaseFactory.Get();
            int flag = db.ExportSales_Login(loginName, password);

            switch (flag)
            {
                case 0:result.Message = "密码错误"; result.Result = false; break;
                case 1:result.Message = "登陆成功"; result.Result = true; break;
                case -1:result.Message = "用户名不存在"; result.Result = false; break;
            }
            return result;
        }
    }
}
