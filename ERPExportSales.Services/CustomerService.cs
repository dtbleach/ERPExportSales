using ERPExportSales.Entities;
using ERPExportSales.Framework;
using ERPExportSales.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Services
{
    public class CustomerService: ICustomerService
    {
        public ICustomerRepository customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
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
