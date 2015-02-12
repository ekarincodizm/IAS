using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using IAS.Common.Logging;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using IAS.Common.Configuration;
using StructureMap;
using StructureMap.Configuration.DSL;
using IAS.Common.Email;
using System.IO;
using log4net.Config;
using System.Configuration;
using IAS.Common.Logging.Services;
using System.Web;
using System.Web.SessionState;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class ErrorTraceLog_ToDatabaseTest
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [TestInitialize]
        public void Init()
        {
            BootStrapper.ConfigureDependencies();

            ApplicationSettingsFactory.InitializeApplicationSettingsFactory
                                 (ObjectFactory.GetInstance<IApplicationSettings>());
            LoggerFactory.SetCurrent(new Log4NetServiceLogFactory());
           
        }

  
        public void TestFirstLog4Net()     
        {

            log4net.ILog log = log4net.LogManager.GetLogger(this.GetType());

            log4net.Config.XmlConfigurator.Configure();
            log4net.Repository.Hierarchy.Hierarchy h = (Hierarchy)log4net.LogManager.GetRepository();
            var adoAppenders = h.GetAppenders().OfType<AdoNetAppender>();
            //adoAppender.ConnectionString = "Data Source=XE;User ID=APPLICATIONDB;Password=password";
            //adoAppender.CommandText = "INSERT INTO LOG4NET (LOG_ID, LOG_DATE, LOG_LEVEL, LOG_IDENTITY, LOG_MESSAGE, LOG_EXCEPTION) VALUES (LOG4NET_SEQ.nextval, :log_date, :log_level, :log_identity, :log_message, :log_exception)";
            foreach (var appender in adoAppenders)
            {
                appender.ConnectionString = ConfigurationManager.ConnectionStrings["OraDB_Log"].ConnectionString;
                appender.ActivateOptions();
            }
            log.Info("hello world!");
        }


        [TestMethod]
        public void ErrorTraceLog_ToDatabase_CanLogDatabase()
        {
            SettingServiceLog();

            LoggerFactory.CreateLog().LogInfo("log TestInfo");

            //LoggerFactory.CreateLog().LogInfo("log TestInfo");
            //LoggerFactory.CreateLog().LogError("log LogError");
            //LoggerFactory.CreateLog().LogWarning("log LogWarning");
            //LoggerFactory.CreateLog().Debug("log LogWarning");

            //Exception ex = new Exception("Exception Error");

            //LoggerFactory.CreateLog().LogError("log LogError Exception", ex);

            //ApplicationException exapp = new ApplicationException("App Error");
            //LoggerFactory.CreateLog().Fatal("dfdkfjdkf", exapp);
        }

        public static HttpContext FakeHttpContext(string url)
        {
            var uri = new Uri(url);
            var httpRequest = new HttpRequest(string.Empty, uri.ToString(),
                                                uri.Query.TrimStart('?'));
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id",
                                            new SessionStateItemCollection(),
                                            new HttpStaticObjectsCollection(),
                                            10, true, HttpCookieMode.AutoDetect,
                                            SessionStateMode.InProc, false);

            SessionStateUtility.AddHttpSessionStateToContext(
                                                 httpContext, sessionContainer);

            return httpContext;
        }
        public void SettingServiceLog() 
        {
            HttpContext.Current = FakeHttpContext("http://stackoverflow/");
            HttpContext.Current.Session["LogingName"] = "UserName";
            HttpContext.Current.Session["OICUserId"] = "12-2345";
            HttpContext.Current.Session["DeptCode"] = "1";
            HttpContext.Current.Session["CompanyCode"] = "111";
            HttpContext.Current.Session["CreatedBy"] = "1254632154879563";
        }

        public LogServiceMessage GetLogMessage(Int32 logLevel, String detail) {
            LogServiceMessage message = new LogServiceMessage()
            {
                TransDate = DateTime.Now,
                LogLevel = logLevel,
                LogingName = "INFO",
                OICUserId = "----",
                DeptCode = "----",
                CompanyCode = "-aa",
                SystemCode = "SORIA",
                SubSystemCode = "SORIA_BACK",
                PrgId = "PROGRAM",
                IpAddress = "192.168.16.23",
                LogHeader = "INFO",
                Detail = detail,
                CreateDate = DateTime.Now,
                CreateBy = "aaaa"
            };

            return message;
        }
        //[TestMethod]
        //public void ErrorTraceLog_ToDatabase_CanLogDatabase2()
        //{

        //    Assert.AreEqual(1, 1);

        //    FileInfo file = new FileInfo("C:dkfjdkfjdifkdf/");

        //    IList<FileInfo> listFileInfo = new List<FileInfo>();
        //    listFileInfo.Add(new FileInfo("C:dkfjdkfjdifkdf/"));


        //    EmailServiceFactory.GetEmailService().SendMail("", "", "", "", listFileInfo);
        //}
    }
}
