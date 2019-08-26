using EntityFramework.Functions;
using ERPExportSales.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPExportSales.Repositories
{
    public partial class ApplicationDbContext
    {
        public const string dbo = nameof(dbo);

        /// <summary>
        /// 外销登录
        /// </summary>
        /// <param name="loginName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [Function(FunctionType.NonComposableScalarValuedFunction, "f外销_登陆校验", Schema = dbo)]
        [return: Parameter(DbType = "int")]
        public int ExportSales_Login(
            [Parameter(DbType = "varchar", Name = "登录名")]string loginName,
            [Parameter(DbType = "varchar", Name = "密码")]string password)
        {
            ObjectParameter loginNameParameter = new ObjectParameter("登录名", loginName);
            ObjectParameter passwordParameter = new ObjectParameter("密码", password);
            return this.ObjectContext().ExecuteFunction<int>("f外销_登陆校验", loginNameParameter, passwordParameter).SingleOrDefault();
        }

        /// <summary>
        /// 外销登录
        /// </summary>
        /// <param name="loginName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public LoginStatus ExportSales_Login_ReturnUserType(string loginName, string password)
        {

            SqlParameter loginNameParameter = new SqlParameter("登录名", loginName);
            SqlParameter passwordParameter = new SqlParameter("密码", password);
            var login = this.Database.SqlQuery<LoginStatus>("select * from dbo.f外销_登陆校验_多返回值(@登录名,@密码)", loginNameParameter, passwordParameter).SingleOrDefault();
            return login;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Function(FunctionType.NonComposableScalarValuedFunction, "f获取员工级别", Schema = dbo)]
        [return: Parameter(DbType = "int")]
        public int GetEmployeeLevel([Parameter(DbType = "varchar", Name = "姓名")]string name)
        {
            ObjectParameter nameParameter = new ObjectParameter("姓名", name);

            return this.ObjectContext().ExecuteFunction<int>("f获取员工级别", nameParameter).SingleOrDefault();
        }


        public int SendEmail(string from, string to, string title, string content, int fid, int returnFlag)
        {
            fid = 0;
            returnFlag = 1;

            SqlParameter[] sqlp = new SqlParameter[]
            {
                new SqlParameter("@fID",fid),
                 new SqlParameter("@发件人",from),
                  new SqlParameter("@收件人",to),
                   new SqlParameter("@标题",title),
                    new SqlParameter("@内容",content),
                     new SqlParameter("@返回标识值",returnFlag)
            };
            var s=this.Database.ExecuteSqlCommand("exec p邮件生成 @fID output,@发件人,@收件人,@标题,@内容,@返回标识值", sqlp);
            return s;
        }

    }


}
