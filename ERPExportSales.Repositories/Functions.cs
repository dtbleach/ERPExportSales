using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Repositories
{
    public static class Functions
    {
        public static int ExportSales_Login(string loginName,string password)
        {
           var nameParameter = loginName != null ?
           new ObjectParameter("用户名", loginName) :
           new ObjectParameter("用户名", typeof(string));

            var passwordParameter = password != null ?
           new ObjectParameter("密码", password) :
           new ObjectParameter("密码", typeof(string));

            ObjectParameter[] param = new ObjectParameter[] 
            {
                nameParameter,
                passwordParameter
            };

            return ((IObjectContextAdapter)this).ObjectContext.
                ExecuteFunction("f外销_登陆校验", param);
        }
    }
}
