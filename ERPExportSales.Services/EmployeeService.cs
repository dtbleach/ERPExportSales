using ERPExportSales.Framework;
using ERPExportSales.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPExportSales.Entities;

namespace ERPExportSales.Services
{
    public class EmployeeService : IEmployeeService
    {

        public IEmployeeRepository employeeRepository;

        public IUnitOfWork unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.employeeRepository = employeeRepository;
        }

        public Employee GetEmployee(string userName)
        {
            return employeeRepository.Get(p => p.LoginName == userName);
        }

        public string GetEmployeeSHAPassword(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return string.Empty;

            var employee=employeeRepository.Get(x => x.LoginName == userName);
            if (employee == null)
                return string.Empty;

            string password = ConvertHelper.BytesToString(employee.Password, Encoding.UTF8);

            return password;
        }
    }
}
