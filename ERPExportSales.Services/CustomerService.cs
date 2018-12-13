using ERPExportSales.Entities;
using ERPExportSales.Framework;
using ERPExportSales.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Services
{
    public class CustomerService: ICustomerService
    {
        public ICustomerRepository customerRepository;

        public IDatabaseFactory databaseFactory;

        public IUnitOfWork unitOfWork;
        public CustomerService(ICustomerRepository customerRepository, IDatabaseFactory databaseFactory, IUnitOfWork unitOfWork)
        {
            this.customerRepository = customerRepository;
            this.databaseFactory = databaseFactory;
            this.unitOfWork = unitOfWork;
        }

        public BizResult<bool> ChangePassword(string loginName, string oldPassword, string newPassword, string confirmPassword)
        {
            SqlParameter paramLoginName = new SqlParameter("@LoginName",loginName);
            SqlParameter paramOldPassword = new SqlParameter("@OldPassword", oldPassword);
            SqlParameter paramNewPassword = new SqlParameter("@NewPassword", newPassword);
            SqlParameter paramConfirmPassword = new SqlParameter("@ConfirmPassword", confirmPassword);
            SqlParameter paramResult = new SqlParameter("@Result",SqlDbType.Int);
            paramResult.Direction = ParameterDirection.Output;
            var db = databaseFactory.Get();
            db.Database.SqlQuery<int>("p外销电商_客户修改密码 @LoginName,@OldPassword,@NewPassword,@ConfirmPassword,@Result output", paramLoginName, paramOldPassword, paramNewPassword, paramConfirmPassword, paramResult).FirstOrDefault();
            BizResult<bool> bizResut = new BizResult<bool>();
            switch ((int)paramResult.Value)
            {
                case 0: bizResut.Result = false;bizResut.Message = "Failed to change password(0)"; break;
                case 1: bizResut.Result = true; bizResut.Message = "Successful password change"; break;
                case -1: bizResut.Result = false; bizResut.Message = "Please enter the same password"; break;
                case -2: bizResut.Result = false; bizResut.Message = "Incorrect account and password"; break;
                case -3: bizResut.Result = false; bizResut.Message = "Failed to change password(-3)"; break;
            }
            return bizResut;
        }

        public Customer GetCustomer(string userName)
        {
            return customerRepository.Get(p => p.LoginName == userName);
        }

        public string GetCustomerSHAPassword(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return string.Empty;

            var customer = customerRepository.Get(x => x.LoginName == userName);
            if (customer == null)
                return string.Empty;

            string password = ConvertHelper.BytesToString(customer.Password, Encoding.UTF8);

            return password;
        }
    }
}
