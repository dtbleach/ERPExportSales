using ERPExportSales.Entities;
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

        public ICustomerRepository customerRepository;

        public ExportSalesUserService(IDatabaseFactory databaseFactory,IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
		{
            this.databaseFactory = databaseFactory;
            this.unitOfWork = unitOfWork;
            this.customerRepository = customerRepository;
        }

        /// <summary>
        /// 外销用户登陆校验
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public BizResult<bool> Login(string loginName, string password,ref int userType)
        {
            BizResult<bool> result = new BizResult<bool>();

            if (string.IsNullOrEmpty(loginName))
            {
                result.Result = false;
                result.Message = "Username is null";
                return result;
            }

            if (string.IsNullOrEmpty(password))
            {
                result.Result = false;
                result.Message = "Password is null";
                return result;
            }

            var db = databaseFactory.Get();
            LoginStatus flag = db.ExportSales_Login_ReturnUserType(loginName, password);

            switch (flag.Login)
            {
                case 0:result.Message = "Password error"; result.Result = false; break;
                case 1:result.Message = "Success"; result.Result = true; break;
                case -1:result.Message = "Username does not exist"; result.Result = false; break;
            }
            userType = flag.UserType;
            return result;
        }

        public BizResult<bool> Modify(string oldPassword,string password, string confirmPassword, string loginName)
        {
            //BizResult<bool> result = new BizResult<bool>();
            //var customer = customerRepository.Get(p => p.LoginName == loginName);
            // if (customer != null)
            //{
            //    if (customer.Password == oldPassword)
            //    {

            //    }
            //}
            return null;
        }

        public BizResult<bool> Modify(string password, string confirmPassword, string loginName)
        {
            return null;
        }
    }
}
