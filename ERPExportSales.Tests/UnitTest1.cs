using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ERPExportSales.Repositories;

namespace ERPExportSales.Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void Login_Test()
        {
            ApplicationDbContext context = new ApplicationDbContext();


            var obj = context.Database.SqlQuery<int>("SELECT [dbo].[f外销_登陆校验]('llh','1qazxsw2')").FirstOrDefaultAsync();
            Assert.AreEqual(1, obj.Result);
        }

    }
}
